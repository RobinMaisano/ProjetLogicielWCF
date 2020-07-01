using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFContract;

namespace WCFMiddleware
{
    class DecryptService : IService
    {
        public MSG ExecuteService(MSG message)
        {
            return BAC.Dispatch(message);
        }
    }
}
