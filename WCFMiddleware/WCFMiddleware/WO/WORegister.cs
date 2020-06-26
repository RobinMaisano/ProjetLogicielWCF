using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFContract;

namespace WCFMiddleware
{
    class WORegister : IWorkflowOrchestrator
    {
        private MSG _message;

        public MSG Execute(MSG message)
        {
            _message = message;
            // Call business logic component

            BLRegister register = new BLRegister();

            string login = (string)_message.data[0];
            string password = (string)_message.data[1];

            //TODO: Check already used login

            //TODO:
            //Hash password


            //TODO: handle groups[]


            //_message.statusOp = register.Register(login, password, new string[] { });
            bool status = register.Register(login, password, new string[] { });

            if (status)
                _message.info = "You went through and got back.";
            else
                _message.info = "Something went wrong.";

            return _message;
        }
    }
}
