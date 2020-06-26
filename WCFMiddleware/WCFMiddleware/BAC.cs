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
        static IWorkflowOrchestrator WO = null;
        static public MSG Dispatch (MSG message)
        {
            //TODO: Check Rights


            //Check Rights + Dispatch to corresponding WorkflowOrchestrator
            switch (message.operationName)
            {
                case "Register":
                    if (message.appVersion == "V1")
                        WO = new WORegister();
                    break;

                case "Login":
                    if (message.appVersion == "V1")
                        WO = new WOLogin();
                    break;

                case "Decrypt":
                    if (message.appVersion == "V1")
                        WO = new WODecrypt();
                    break;

                default:
                    WO = null;
                    break;
            }

            if (WO == null)
            {
                message.info = "Unsupported service version";
                message.statusOp = false;
                return message;
            }

            return WO.Execute(message);
        }
    }
}
