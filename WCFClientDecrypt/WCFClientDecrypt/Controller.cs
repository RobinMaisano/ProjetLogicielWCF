using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClientDecrypt.proxy;

namespace WCFClientDecrypt
{
    class Controller
    {
        private Connection connection;
        private MSG msg;
        private User user;
        public Controller()
        {
            this.msg = new MSG();
            this.connection = new Connection();
        }
        public Controller(User user)
        {
            this.msg = new MSG();
            this.connection = new Connection();
            this.user = user;
        }

        public MSG m_helloworld(MSG msg)
        {
            this.msg = msg;
            this.msg.operationName = "Hello world";

            this.msg = this.connection.m_send(this.msg);

            return this.msg;
        }

        public MSG m_register(MSG msg)
        {
            this.msg = msg;
            this.msg.operationName = "Register";

            this.msg = this.connection.m_send(this.msg);
            this.user.SettokenUser(this.msg.tokenUser);
            
            return this.msg;
        }

        public MSG m_login(MSG msg)
        {
            this.msg = msg;
            this.msg.operationName = "Login";

            this.msg = this.connection.m_send(this.msg);
            this.user.SettokenUser(this.msg.tokenUser);

            return this.msg;
        }

        public MSG m_decrypt(MSG msg)
        {
            this.msg = msg;
            this.msg.operationName = "Decrypt";
            this.msg.tokenUser = this.user.GettokenUser();

            this.msg = this.connection.m_send(this.msg);

            return this.msg;
        }
    }
}
