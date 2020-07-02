using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using WCFContract;
using WCFDAL;
using WCFDAL.Models;

namespace WCFMiddleware
{
    class WODecrypt : IWorkflowOrchestrator
    {
        private BLDecrypt BLDecrypt = new BLDecrypt();
        private FileStatusHandler FSH = FileStatusHandler.Instance;

        public MSG Execute(MSG message)
        {
            try
            {
                string login = DAO.Instance.Users.Where(u => u.Token == message.tokenUser).FirstOrDefault().Login;

                int i = 0;
                string[] fileNames = new string[message.data.Length];
                foreach (Dictionary<string, string> file in message.data)
                {
                    file["title"] = login + "." + file["title"];
                    fileNames[i++] = file["title"];
                }

                Thread decryptProcess = new Thread(_ => { DecryptProcess(message); });
                decryptProcess.Start();

                Thread.Sleep(100); // To be sure that message has been consummed by Thread before modifying it
                message.info = "Decryption is running";
                message.statusOp = true;
                message.data = fileNames;
            }
            catch (Exception)
            {
                message.info = "An error occured when launching decryption, please try again later.";
                message.statusOp = false;
            }

            return message;
        }

        private void DecryptProcess(MSG message)
        {

            string[,] filesToDecrypt = new string[2, message.data.Length];


            int iData = 0;
            foreach (Dictionary<string, string> file in message.data)
            {
                filesToDecrypt[0, iData] = file["title"];
                filesToDecrypt[1, iData] = file["content"];
                FSH.FileStatus.Add(filesToDecrypt[0, iData], new DecryptionInformations { FileName = filesToDecrypt[0, iData], OriginalFileContent = file["content"], Decrypted = false });
                iData++;
            }

            int maxThread = GetProcessorNb();
            int startedThread = 0;

            Thread[] runningThreadList = new Thread[maxThread];

            while (startedThread < (filesToDecrypt.Length / 2) && startedThread < maxThread)
            {
                Thread threadToStart = new Thread(() => BLDecrypt.DecryptFile(filesToDecrypt[0, startedThread], filesToDecrypt[1, startedThread]));
                runningThreadList[startedThread] = threadToStart;
                runningThreadList[startedThread].Start();
                Thread.Sleep(100);
                startedThread++;
            }

            while (startedThread < filesToDecrypt.Length / 2)
            {
                for (int i = 0; i < runningThreadList.Length; i++)
                {
                    if (runningThreadList[i] != null && runningThreadList[i].ThreadState == ThreadState.Stopped && startedThread < filesToDecrypt.Length / 2)
                    {
                        runningThreadList[i] = new Thread(() => BLDecrypt.DecryptFile(filesToDecrypt[0, startedThread], filesToDecrypt[1, startedThread]));
                        runningThreadList[i].Start();
                        Thread.Sleep(100);
                        startedThread++;
                    }
                }
                Thread.Sleep(1000);
            }

            // Join all threads
            foreach (Thread th in runningThreadList)
                th.Join();

            // Generate PDF
            StringBuilder sb = BuildPDF(filesToDecrypt);

            // Send Email
            sendMail(DAO.Instance.Users.Where(u => u.Token == message.tokenUser).FirstOrDefault(), sb);
        }

        private StringBuilder BuildPDF(string[,] filesDecrypted)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<h1>Decryption report</h1>");
            sb.Append("<br>");
            sb.Append("<h2>File(s) submitted</h2>");
            sb.Append("<ul>");
            for (int i = 0; i < filesDecrypted.Length / 2; i++)
            {
                sb.Append($"<li>{ filesDecrypted[0, i] }</li>");
            }
            sb.Append("</ul>");
            sb.Append("<br>");
            sb.Append("<h2>Results</h2>");
            sb.Append("<ul>");
            for (int i = 0; i < filesDecrypted.Length / 2; i++)
            {
                DecryptionInformations infos = FSH.FileStatus[filesDecrypted[0, i]];
                sb.Append("<li>");
                sb.Append($"<h3>{ infos.FileName }</h3>");
                sb.Append($"<p>Key: { infos.Key }</p>");
                sb.Append($"<p>Confidence: { infos.Trust }</p>");
                sb.Append($"<p>Secret Information: { infos.SecretInfo }</p>");
                sb.Append($"<p>Content: { BLDecrypt.DecryptStringToString(infos.Key, infos.OriginalFileContent) }</p>");
                sb.Append("</li>");
                sb.Append("<br>");
            }
            sb.Append("</ul>");
            return sb;
        }

        private void sendMail(User user, StringBuilder sb)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlParser = new HTMLWorker(pdfDoc);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();
                        htmlParser.Parse(sr);
                        pdfDoc.Close();
                        byte[] bytes = memoryStream.ToArray();
                        memoryStream.Close();

                        MailMessage mm = new MailMessage("cesiwcf@gmail.com", user.Email);
                        mm.Subject = "Decryption Results";
                        mm.Body = "Find results in pdf attached";
                        mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "decryptionResults.pdf"));
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential credential = new NetworkCredential();
                        credential.UserName = "cesiwcf@gmail.com";
                        credential.Password = "Cesi123$";
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = credential;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                }
            }
        }

        private int GetProcessorNb()
        {
            return Environment.ProcessorCount;
        }
    }
}
