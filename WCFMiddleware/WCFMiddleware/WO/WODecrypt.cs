using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCFContract;
using WCFDAL;
using WCFDAL.Models;

namespace WCFMiddleware
{
    class WODecrypt : IWorkflowOrchestrator
    {
        public MSG Execute(MSG message)
        {
            try
            {
                Thread decryptProcess = new Thread(_ => { DecryptProcess(message); });
                decryptProcess.Start();

                message.info = "Decryption is running";
                message.statusOp = true;
            }
            catch (Exception)
            {
                message.info = "An error occured when launching decryption, please try again later.";
                message.statusOp = false;
            }

            return message;
        }

        private int GetProcessorNb()
        {
            return Environment.ProcessorCount;
        }

        private void DecryptProcess(MSG message)
        {
            BLDecrypt BLDecrypt = new BLDecrypt();

            string login = DAO.Instance.Users.Where(u => u.Token == message.tokenUser).FirstOrDefault().Login;

            String[,] filesToDecrypt = new string[2, message.data.Length];

            FileStatusHandler fileStatusHandler = FileStatusHandler.Instance;

            int iData = 0;
            foreach (Dictionary<string, string> file in message.data)
            {
                filesToDecrypt[0, iData] = login + "." + file["title"];
                filesToDecrypt[1, iData] = file["content"];
                fileStatusHandler.FileStatus.Add(filesToDecrypt[0, iData], new DecryptionInformations { FileName = filesToDecrypt[0, iData], OriginalFileContent = file["content"], Decrypted = false });
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

            // Generate PDF

            // Send Email

        }
    }
}
