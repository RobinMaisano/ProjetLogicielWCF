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
        private MSG _message;
        private readonly BLLogin BLLogin;

        public WOLogin()
        {
            this.BLLogin = new BLLogin();
        }

        public MSG Execute(MSG message)
        {
            _message = message;
            try
            {
                bool logged = BLLogin.Login(_message.data[0].ToString(), _message.data[1].ToString());

                if (logged)
                {
                    _message.tokenUser = BLLogin.GetToken(_message.data[0].ToString());

                    if (_message.tokenUser == null)
                    {
                        _message.info = "Error while generating token";
                        _message.statusOp = false;
                    }
                    else
                    {
                        _message.info = "Logged in successfully";
                        _message.statusOp = true;
                    }
                }
                else
                {
                    _message.info = "Incorrect Login or Password";
                    _message.statusOp = false;
                }
            }
            catch (Exception ex)
            {
                _message.info = $"Exception during login: {ex.Message}";
                _message.statusOp = false;
            }

            return _message;
        }
    }
}
