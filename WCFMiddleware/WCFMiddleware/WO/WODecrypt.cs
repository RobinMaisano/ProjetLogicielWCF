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
            BLDecrypt BLDecrypt = new BLDecrypt();

            //Thread t1 = new Thread(() => BLDecrypt.DecryptString("test"));

            int nbData = message.data.Length;

            string login = DAO.Instance.Users.Where(u => u.Token == message.tokenUser).FirstOrDefault().Login;

            String[,] filesToDecrypt = new string[2, nbData/2];


            FileStatusHandler fileStatusHandler = FileStatusHandler.Instance;
            int iData = 0, iDecrypt = 0;
            while (iData < message.data.Length) {
                filesToDecrypt[0, iDecrypt] = login + "." + message.data[iData++].ToString();
                filesToDecrypt[1, iDecrypt] = message.data[iData].ToString();
                fileStatusHandler.FileStatus.Add(filesToDecrypt[0, iDecrypt], new DecryptionInformations { FileName = filesToDecrypt[0, iDecrypt], OriginalFileContent = filesToDecrypt[1, iDecrypt], Decrypted = false });
                iData++; iDecrypt++;
            }

            int maxThread = GetProcessorNb();
            int startedThread = 0;

            Thread[] runningThreadList = new Thread[maxThread];

            while (startedThread < (filesToDecrypt.Length/2) && startedThread < maxThread)
            {
                Thread threadToStart = new Thread(() => BLDecrypt.DecryptFile(filesToDecrypt[0, startedThread], filesToDecrypt[1, startedThread]));
                runningThreadList[startedThread] = threadToStart;
                runningThreadList[startedThread].Start();
                Thread.Sleep(100);
                startedThread++;
            }


            while (startedThread < filesToDecrypt.Length/2)
            {
                for (int i = 0; i < runningThreadList.Length; i++)
                {
                    if (runningThreadList[i] != null && runningThreadList[i].ThreadState == ThreadState.Stopped && startedThread < filesToDecrypt.Length/2)
                    {
                        runningThreadList[i] = new Thread(() => BLDecrypt.DecryptFile(filesToDecrypt[0, startedThread], filesToDecrypt[1, startedThread]));
                        runningThreadList[i].Start();
                        Thread.Sleep(100);
                        startedThread++;
                    }
                }
                Thread.Sleep(1000);
            }

            //string result = BLDecrypt.DecryptStringTest(" ./).43b");

            //message.info = result;
            message.info = "Running";
            message.statusOp = true;
            return message;
        }

        private int GetProcessorNb()
        {
            return Environment.ProcessorCount;
        }

    }
}
