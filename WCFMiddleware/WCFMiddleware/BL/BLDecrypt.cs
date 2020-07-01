using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCFMiddleware.JEEWebservice;

namespace WCFMiddleware
{
    class BLDecrypt
    {
        readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        readonly int keyLength = 4;
        

        public void DecryptFile(string fileName, string fileContent)
        {
            int i = 0;
            int maximumKey = (int)Math.Pow(alphabet.Length, keyLength);

            DecryptionInformations decryptionInformations; 
            FileStatusHandler FSH = FileStatusHandler.Instance;
            FSH.FileStatus.TryGetValue(fileName, out decryptionInformations);

            while (i < maximumKey && decryptionInformations.Decrypted != true)
            {
                Console.WriteLine(FSH.changed);
                //Generate Key
                string key = GenerateKey(i);

                //Decrypt String
                string contentDecrypted = DecryptString(key, fileContent);


                //Send Decrypted Content to JavaEE
                SendToJava(fileName, contentDecrypted, key);

                i++;
                FSH.FileStatus.TryGetValue(fileName, out decryptionInformations);

                //Console.WriteLine(contentDecrypted);
            }
        }

        public string GenerateKey(int keyNb)
        {
            return alphabet[(int)(keyNb / Math.Pow(alphabet.Length, 3))] + ""
                                    + alphabet[(int)(keyNb / Math.Pow(alphabet.Length, 2))] + ""
                                    + alphabet[(int)(keyNb / alphabet.Length)] + ""
                                    + alphabet[keyNb % alphabet.Length];
        }

        public string DecryptString(string key, string textToDecrypt)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            Byte[] bytesString = ascii.GetBytes(textToDecrypt);
            Byte[] bytesKey = ascii.GetBytes(key);
            Byte[] bytesDecrypted = new Byte[bytesString.Length];

            for (int i = 0; i < bytesString.Length; i++)
                bytesDecrypted[i] = (Byte)(bytesString[i] ^ bytesKey[i % keyLength]);

            return ascii.GetString(bytesDecrypted);
        }

        public void SendToJava(string fileName, string content, string key)
        {

            FileReceiverEndpClient client = new FileReceiverEndpClient();
            Console.WriteLine("content : "+content);
            client.messageReader(content, key, fileName);

            Thread.Sleep(200);
        }
    }
}
