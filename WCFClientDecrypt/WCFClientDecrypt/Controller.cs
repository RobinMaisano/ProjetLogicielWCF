using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            if (ConfigurationManager.AppSettings.Get("test") == "y")
            {
                this.msg.statusOp = true;
                this.msg.info = "You went through and got back";
                return this.msg;
            }

            this.msg = this.connection.m_send(this.msg);
            this.user.SettokenUser(this.msg.tokenUser);
            
            return this.msg;
        }

        public MSG m_login(MSG msg)
        {
            this.msg = msg;
            this.msg.operationName = "Login";

            if (ConfigurationManager.AppSettings.Get("test") == "y")
            {
                this.msg.statusOp = true;
                this.msg.info = "Logged in successsfully";
                this.msg.tokenUser = "itsATokenUserISwear";
                this.user.SettokenUser(this.msg.tokenUser);
                return this.msg;
            }

            this.msg = this.connection.m_send(this.msg);
            this.user.SettokenUser(this.msg.tokenUser);

            return this.msg;
        }

        public MSG m_decrypt(MSG msg)
        {
            this.msg = msg;
            this.msg.operationName = "Decrypt";

            if (ConfigurationManager.AppSettings.Get("test") == "y")
            {
                this.msg.statusOp = true;
                this.msg.info = "The request was accepted";
                this.msg.tokenUser = "itsATokenUserISwear";
                Object[] predata = new Object[this.msg.data.Count()];
                int k = 0;
                foreach (Dictionary<string, string> fileDict in this.msg.data)
                {
                    fileDict["title"] = this.user.Getlogin() + "." + fileDict["title"];
                    predata[k] = fileDict;
                    k++;
                }
                this.msg.data = predata;
                this.user.SettokenUser(this.msg.tokenUser);
                return this.msg;
            }

            this.msg.tokenUser = this.user.GettokenUser();

            this.msg = this.connection.m_send(this.msg);

            return this.msg;
        }

        public MSG m_checkIsDecrypted(MSG msg)
        {
            msg.operationName = "IsDecrypted";

            if (ConfigurationManager.AppSettings.Get("test") == "y")
            {
                msg.statusOp = true;
                msg.info = "The result are out";
                Object[] predata = new Object[msg.data.Count()];
                int k = 0;
                foreach (Dictionary<string, string> fileDict in msg.data)
                {
                    Dictionary<string, string> resultDict = new Dictionary<string, string>();
                    resultDict.Add("title", fileDict["title"]);
                    resultDict.Add("content", fileDict["content"]);
                    resultDict.Add("key", "AAA" + k.ToString());
                    resultDict.Add("trust", (100 - k).ToString());
                    if (msg.data.Count() == k + 2)
                    {
                        resultDict.Add("secretInfo", "This is secret");
                    }
                    else
                    {
                        resultDict.Add("secretInfo", "");
                    }
                    predata[k] = resultDict;
                    k++;
                }
                msg.data = predata;
                return msg;
            }

            this.msg.tokenUser = this.user.GettokenUser();

            this.msg = this.connection.m_send(this.msg);

            return this.msg;
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
