using System;
using System.ServiceModel.Configuration;
using WCFContract;

namespace WCFMiddleware
{
    internal class WODecrypted : IWorkflowOrchestrator
    {
        public MSG Execute(MSG message)
        {
            try
            {
                FileStatusHandler FSH = FileStatusHandler.Instance;

                DecryptionInformations infos = FSH.FileStatus[message.data[0].ToString()];

                infos.Key = message.data[1].ToString();
                infos.Trust = double.Parse(message.data[2].ToString());
                infos.SecretInfo = message.data[3] == null ? string.Empty : message.data[3].ToString();

                FSH.FileStatus[infos.FileName] = infos;

                message.statusOp = true;
                message.info = "Updated correctly";
            }
            catch (Exception)
            {
                message.statusOp = false;
                message.info = "Something went wrong when updating Status Handler";
            }

            return message;
        }
    }
}