using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFMiddleware
{
    public struct DecryptionInformations
    {
        public string FileName;
        public string OriginalFileContent;
        public bool Decrypted;
        public string Key;
        public double Trust;
        public string SecretInfo;

    }
}
