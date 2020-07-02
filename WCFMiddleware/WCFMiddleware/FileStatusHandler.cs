using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFMiddleware
{
    class FileStatusHandler
    {

        private static FileStatusHandler _instance = null;
        private static readonly object _lock = new object();
        public static FileStatusHandler Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new FileStatusHandler();
                    }
                    return _instance;
                }
            }
        }

        public Dictionary<string, DecryptionInformations> FileStatus;

        public FileStatusHandler()
        {
            FileStatus = new Dictionary<string, DecryptionInformations>();
        }

    }
}
