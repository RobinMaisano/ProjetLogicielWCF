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
        private BLLogin BLLogin;

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
                    string login = _message.data[0].ToString();
                    _message.data = new object[1];
                    _message.data[0] = BLLogin.GetToken(login);

                    if (_message.data[0] == null)
                    {
                        _message.info = "Error while generating token";
                        _message.statusOp = false;
                    }
                    else
                    {
                        _message.info = "Logged in succesfully";
                        _message.statusOp = true;
                        Console.WriteLine("Returned token: " + _message.data[0]);
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

            Console.WriteLine(_message.info);
            return _message;
        }
    }
}
