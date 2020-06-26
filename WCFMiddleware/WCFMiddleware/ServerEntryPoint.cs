﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFContract;

namespace WCFMiddleware
{
    public class ServerEntryPoint : IServerEntryPoint
    {
        private string _tokenApp = "4ppT0k3n";
        private MSG _message;
        private IService _service;

        public MSG m_service(MSG message)
        {
            Console.WriteLine("Message received");
            _message = message;

            // App Token Checking
            if (!CheckTokenApp(_message.tokenApp))
            {
                _message.info = "Wrong application";
                _message.statusOp = false;
                return _message;
            }

            switch (_message.operationName)
            {
                case "Register":
                    _service = new RegisterService();
                    break;
                case "Login":
                    _service = new LoginService();
                    break;
                //case "Decrypt":
                //    _service = new RegisterService();
                //    break;
                default:
                    _service = null;
                    break;
            }
            
            // If service does not exists
            if (_service == null)
            {
                _message.info = "Wrong operation";
                _message.statusOp = false;
                return _message;
            }

            return _service.ExecuteService(_message);

        }

        public bool CheckTokenApp(string tokenApp)
        {
            return tokenApp == _tokenApp ? true : false;
        }

    }
}