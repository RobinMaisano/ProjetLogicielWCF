using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFContract;
using WCFDAL;
using WCFDAL.Models;

namespace WCFMiddleware
{
    static class BAC
    {
        static IWorkflowOrchestrator WO = null;
        static public MSG Dispatch (MSG message)
        {

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

                case "Decrypted":
                    if (message.appVersion == "V1")
                        WO = new WODecrypted();
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

            //Check Rights if not logging in or registering
            if (message.operationName != "Register" && message.operationName != "Login" )
            {
                message = CheckRights(message);

                if (message.statusOp == false)
                    return message;
            }

            return WO.Execute(message);
        }
        

        private static MSG CheckRights(MSG message)
        {
            DAO DAO = DAO.Instance;

            // If no user token
            if (message.tokenUser == null)
            {
                message.info = "Please login before trying to access this service.";
                message.statusOp = false;
                return message;
            }

            // If no user with corresponding token
            User user = DAO.Users.Where(u => u.Token == message.tokenUser).FirstOrDefault();

            if (user == null)
            {
                message.info = "Could not find token, please login and try again.";
                message.statusOp = false;
                return message;
            }

            // If insufficient privileges
            Service service = DAO.Services.Where(s => s.Name == message.operationName).FirstOrDefault();

            if (!user.Privileges.Contains(service.Privilege))
            {
                message.info = "Access denied";
                message.statusOp = false;
                return message;
            }

            // If User found with token & sufficient privileges
            message.statusOp = true;
            return message;
        }
    }
}
