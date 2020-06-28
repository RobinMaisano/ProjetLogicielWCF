using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFClientDecrypt
{
    class User
    {
        private string login;
        private string password;
        private string tokenUser;
        public User() { }

        public string getlogin()
        {
            return this.login;
        }
        public void setlogin(string login)
        {
            this.login = login;
        }
        public string getpassword()
        {
            return this.password;
        }
        public void setpassword(string password)
        {
            this.password = password;
        }
        public string gettokenUser()
        {
            return this.tokenUser;
        }
        public void settokenUser(string tokenUser)
        {
            this.tokenUser = tokenUser;
        }
    }
}
