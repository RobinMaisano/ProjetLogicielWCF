using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFContract;

namespace WCFMiddleware
{
    class RegisterService : IService
    {
        public MSG ExecuteService(MSG message)
        {

            // Launch Service => Reach BAC Layer
            return BAC.Dispatch(message);

            //throw new NotImplementedException();
        }
    }
}
