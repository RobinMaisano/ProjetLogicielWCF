using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFContract;

namespace WCFMiddleware
{
    class WOLogin : IWorkflowOrchestrator
    {
        private MSG message;
        private BLLogin BLLogin;

        public WOLogin()
        {
            this.BLLogin = new BLLogin();
        }

        public MSG Execute(MSG message)
        {
            throw new NotImplementedException();
        }
    }
}
