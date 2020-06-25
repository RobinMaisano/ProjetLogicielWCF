using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFContract;

namespace WCFMiddleware
{
    public class ServerEntryPoint : IServerEntryPoint
    {
        private MSG _message;
        public MSG m_service(MSG message)
        {
            Console.WriteLine("Message received");

            if (message.operationName == "Register")
            {
                IService service = new RegisterService();
                //_message = service.ExecuteService(message);
                return service.ExecuteService(message);
            }
            else
            {
                _message = message;
                _message.info = "OperationName != Register";
                _message.statusOp = false;
            }


            return _message;
            //throw new NotImplementedException();

            //return 

        }
    }
}
