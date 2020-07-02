using System.Collections.Generic;
using WCFContract;

namespace WCFMiddleware
{
    internal class WOIsDecrypted : IWorkflowOrchestrator
    {
        public MSG Execute(MSG message)
        {
            FileStatusHandler FSH = FileStatusHandler.Instance;
            BLDecrypt BLDecrypt = new BLDecrypt();
            bool allDecrypted = true;
            int i = 0, nbDecrypted = 0;
            Dictionary<string, string>[] results = new Dictionary<string, string>[message.data.Length];
            foreach (string fileName in message.data)
            {
                if (FSH.FileStatus[fileName].Decrypted == true)
                {
                    results[i].Add("title", FSH.FileStatus[fileName].FileName);
                    results[i].Add("content", BLDecrypt.DecryptStringToString(FSH.FileStatus[fileName].Key, FSH.FileStatus[fileName].OriginalFileContent));
                    results[i].Add("key", FSH.FileStatus[fileName].Key);
                    results[i].Add("trust", FSH.FileStatus[fileName].Trust.ToString());
                    results[i].Add("secretInfo", FSH.FileStatus[fileName].SecretInfo);

                    nbDecrypted++;
                    i++;
                }
                else
                {
                    allDecrypted = false;
                    i++;
                }
            }

            if (allDecrypted)
            {
                message.data = results;
                message.statusOp = true;
                message.info = "All file you asked for were decrypted.";
            }
            else
            {
                message.statusOp = false;
                message.info = $"{nbDecrypted} of the {message.data.Length} you asked for are decrypted. Wait until they are all done.";
            }

            return message;
        }
    }
}