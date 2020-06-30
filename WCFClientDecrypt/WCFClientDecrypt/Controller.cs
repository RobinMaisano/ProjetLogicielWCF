using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
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
            if (ConfigurationManager.AppSettings.Get("test") == "y")
            {
                this.connection.Set_middleware_test(new Fake_middleware_test(this.user));
            }
        }

        public MSG m_helloworld(MSG msg)
        {
            this.msg = msg;
            this.msg.operationName = "Hello world";

            this.msg = this.connection.m_send(this.msg);
            this.user.SettokenUser(this.msg.tokenUser);

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

        public MSG m_checkIsDecrypted(MSG msg)
        {
            //this.msg = msg;
            msg.operationName = "IsDecrypted";
            msg.tokenUser = this.user.GettokenUser();

            msg = this.connection.m_send(msg);


            return msg;
        }

        public MSG m_checkIsDecrypted_loop(MSG msg)
        {
            msg.statusOp = false;
            while (msg.statusOp == false)
            {
                msg = this.m_checkIsDecrypted(msg);
                //Thread.Sleep(30000);
                //It's long when testing
                Thread.Sleep(5000);
            }
            return msg;
        }

        public string[] GetListFile()
        {
            string location = @"..\..\fileToDecrypt";
            if (ConfigurationManager.AppSettings.Get("fileLocation") != "")
            {
                location = ConfigurationManager.AppSettings.Get("fileLocation");
            }
            string[] listFile = Directory.GetFiles(location);
            return listFile;
        }

        public string GetFileContent(string path)
        {
            String content = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(path))
                {
                    // Read the stream to a string, and write the string to the console.
                    content = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return content;
        }
    }
}
