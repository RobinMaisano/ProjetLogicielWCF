using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFClientDecrypt.proxy;

namespace WCFClientDecrypt
{
    class Fake_middleware_test
    {
        private MSG msg;
        private User user;
        private int countRequestIsDecrypted;
        public Fake_middleware_test(User user)
        {
            this.msg = new MSG();
            this.user = user;
            this.countRequestIsDecrypted = 0;
        }

        public MSG m_service(MSG msg)
        {
            this.msg = msg;
            switch (this.msg.operationName)
            {
                case "Register":
                    this.msg.statusOp = true;
                    this.msg.info = "You went through and got back";
                    this.msg.tokenUser = "itsATokenUserISwear";
                    break;
                case "Login":
                    this.msg.statusOp = true;
                    this.msg.info = "Logged in successsfully";
                    this.msg.tokenUser = "itsATokenUserISwear";
                    break;
                case "Decrypt":
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
                    break;
                case "IsDecrypted":
                    if (this.countRequestIsDecrypted >= 3)
                    {
                        this.msg.statusOp = true;
                        this.msg.info = "The result are out";
                        Object[] pre_data = new Object[this.msg.data.Count()];
                        int j = 0;
                        foreach (Dictionary<string, string> fileDict in this.msg.data)
                        {
                            Dictionary<string, string> resultDict = new Dictionary<string, string>();
                            resultDict.Add("title", fileDict["title"]);
                            resultDict.Add("content", fileDict["content"]);
                            resultDict.Add("key", "AAA" + j.ToString());
                            resultDict.Add("trust", (100 - j).ToString());
                            if (this.msg.data.Count() == (j / 2) + (j % 2))
                            {
                                resultDict.Add("secretInfo", "This is secret");
                            }
                            else
                            {
                                resultDict.Add("secretInfo", "");
                            }
                            pre_data[j] = resultDict;
                            j++;
                        }
                        this.msg.data = pre_data;
                        this.countRequestIsDecrypted = 0;
                    }
                    else
                    {
                        this.msg.statusOp = false;
                        this.msg.info = this.countRequestIsDecrypted + " files have been decrypted";
                        this.countRequestIsDecrypted++;
                    }
                    
                    break;
                default:
                    this.msg.statusOp = false;
                    this.msg.info = "this operation does'nt exist";
                    break;
            }
            return this.msg;
        }
    }
}
