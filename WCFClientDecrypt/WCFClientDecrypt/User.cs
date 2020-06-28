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

        public string Getlogin()
        {
            return this.login;
        }
        public void Setlogin(string login)
        {
            this.login = login;
        }
        public string Getpassword()
        {
            return this.password;
        }
        public void Setpassword(string password)
        {
            this.password = password;
        }
        public string GettokenUser()
        {
            return this.tokenUser;
        }
        public void SettokenUser(string tokenUser)
        {
            this.tokenUser = tokenUser;
        }

        public void ResetValues()
        {
            this.login = "";
            this.password = "";
            this.tokenUser = "";
        }
    }
}
