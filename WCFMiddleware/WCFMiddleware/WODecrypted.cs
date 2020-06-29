using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFContract;

namespace WCFMiddleware
{
    class WODecrypted : IWorkflowOrchestrator
    {
        public MSG Execute(MSG message)
        {
            try
            {
                FileStatusHandler statusHandler = FileStatusHandler.Instance;
                DecryptionInformations informations = new DecryptionInformations
                {
                    FileName = message.data[0].ToString(),
                    Key = message.data[1].ToString(),
                    Confidence = double.Parse(message.data[2].ToString()),
                    Decrypted = true,
                    SecretInformation = message.data.Length == 4 ? message.data[3].ToString() : string.Empty
                };

                statusHandler.FileStatus[informations.FileName] = informations;

                Console.WriteLine("Decrypted: " + informations.FileName);

                message.statusOp = true;
                message.info = $"Decryption {informations.FileName} confirmed";
            }
            catch (Exception)
            {
                message.statusOp = false;
                message.info = "Error during decryption validation";
            }

            return message;
        }
    }
}
