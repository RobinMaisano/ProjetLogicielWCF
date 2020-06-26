using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFMiddleware
{
    class BLDecrypt
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        int keyLength = 4;
        int maximumKey;
        
        public void DecryptString(string stringToDecrypt)
        {
            maximumKey = (int)Math.Pow(alphabet.Length, keyLength);

            for (int i = 0; i < maximumKey; i++)
            {

                //Generate Key
                string key = alphabet[(int)(i / Math.Pow(alphabet.Length, 3))] + ""
                                    + alphabet[(int)(i / Math.Pow(alphabet.Length, 2))] + ""
                                    + alphabet[(int)(i / alphabet.Length)] + ""
                                    + alphabet[i % alphabet.Length];

                //Decrypt String
                ASCIIEncoding ascii = new ASCIIEncoding();
                Byte[] bytesString = ascii.GetBytes(stringToDecrypt);
                Byte[] bytesKey = ascii.GetBytes(key);
                Byte[] bytesDecrypted = new Byte[bytesString.Length];

                for (int j = 0; j < bytesString.Length; j++)
                    bytesDecrypted[i] = (Byte)(bytesString[i] ^ bytesKey[i % keyLength]);

                string stringDecrypted = ascii.GetString(bytesDecrypted);

                //Send Decrypted to JavaEE
                Console.WriteLine(stringDecrypted);
                
            }


        }
        public string DecryptStringTest(string stringToDecrypt)
        {
            maximumKey = (int)Math.Pow(alphabet.Length, keyLength);
            maximumKey = 3;

            string lastKey = "";
            string lastDecryption = "";
            Console.WriteLine(stringToDecrypt);
            for (int i = 0; i < maximumKey; i++)
            {

                //Generate Key
                string key = alphabet[(int)(i / Math.Pow(alphabet.Length, 3))] + ""
                                    + alphabet[(int)(i / Math.Pow(alphabet.Length, 2))] + ""
                                    + alphabet[(int)(i / alphabet.Length)] + ""
                                    + alphabet[i % alphabet.Length];

                //Decrypt String
                ASCIIEncoding ascii = new ASCIIEncoding();
                Byte[] bytesString = ascii.GetBytes(stringToDecrypt);
                Byte[] bytesKey = ascii.GetBytes(key);
                Byte[] bytesDecrypted = new Byte[bytesString.Length];

                Console.WriteLine("Byte length: " + bytesString.Length);

                for (int j = 0; j < bytesString.Length; j++)
                    bytesDecrypted[j] = (Byte)(bytesString[j] ^ bytesKey[j % keyLength]);

                string stringDecrypted = ascii.GetString(bytesDecrypted);

                //Send Decrypted to JavaEE
                Console.WriteLine($"Key: {key}, String: {stringDecrypted}");

                bytesDecrypted.ToList().ForEach(b => Console.WriteLine(b));

                lastKey = key;
                lastDecryption = stringDecrypted;
            }


            return $"Key: {lastKey}, String: {lastDecryption}";

        }

    }
}
