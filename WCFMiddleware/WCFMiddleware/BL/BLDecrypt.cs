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
                //string contentDecrypted = DecryptString(key, fileContent);
                string contentDecrypted = DecryptStringToString(key, fileContent);


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
                    + alphabet[(int)((keyNb % Math.Pow(alphabet.Length, 3)) / Math.Pow(alphabet.Length, 2))] + ""
                    + alphabet[(int)((keyNb % Math.Pow(alphabet.Length, 2)) / Math.Pow(alphabet.Length, 1))] + ""
                    + alphabet[keyNb % alphabet.Length];
        }

        public Byte[] DecryptStringToByteArray(string key, string textToDecrypt)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            Byte[] bytesString = ascii.GetBytes(textToDecrypt);
            Byte[] bytesKey = ascii.GetBytes(key);
            Byte[] bytesDecrypted = new Byte[bytesString.Length];

            for (int i = 0; i < bytesString.Length; i++)
                bytesDecrypted[i] = (Byte)(bytesString[i] ^ bytesKey[i % keyLength]);

            return bytesDecrypted;
        }

        public string DecryptStringToString(string key, string textToDecrypt)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < textToDecrypt.Length; i++)
                sb.Append((char) (textToDecrypt[i] ^ key[i % keyLength]));

            return sb.ToString();
        }

        public void SendToJava(string fileName, string content, string key)
        {
            Byte[] bytesContent = Encoding.UTF8.GetBytes(content);

            FileReceiverEndpClient client = new FileReceiverEndpClient();
            Console.WriteLine("content : "+content);
            client.messageReader(bytesContent, key, fileName);

            Thread.Sleep(200);
        }
    }
}
