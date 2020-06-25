using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFContract;

namespace WCFMiddleware
{
    static class BAC
    {
        static IWorkflowOrchestrator WO;
        static public MSG Dispatch (MSG message)
        {
            //Check Rights + Dispatch to corresponding WorkflowOrchestrator

            //TODO: Check Rights

            WO = new WORegister();
            return WO.Execute(message);

            //throw new NotImplementedException();
        }
    }
}
