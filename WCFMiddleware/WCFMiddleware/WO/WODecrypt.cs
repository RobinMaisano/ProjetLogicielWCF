using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WCFContract;

namespace WCFMiddleware
{
    class WODecrypt : IWorkflowOrchestrator
    {
        public MSG Execute(MSG message)
        {
            BLDecrypt BLDecrypt = new BLDecrypt();

            string result = BLDecrypt.DecryptStringTest(" ./).43b");

            message.info = result;
            message.statusOp = true;
            return message;

            throw new NotImplementedException();
        }
    }
}
