using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFDAL;
using WCFDAL.Models;

namespace WCFMiddleware
{
    class BLLogin
    {
        public bool Login(string login, string password)
        {

            DAO DAO = DAO.Instance;

            User user = DAO.Users.Where(u => u.Login == login).FirstOrDefault();

            //TODO: Crypt password

            if (user == null)
                return false;
            else if (user.Login == login && user.Password == password)
                return true;
            else
                return false;
        }

        public string GetToken(string login)
        {
            DAO DAO = DAO.Instance;

            string token = GenerateToken();

            User user = DAO.Users.Where(u => u.Login == login).FirstOrDefault();
            user.Token = token;
            DAO.SaveChanges();

            return token;
        }

        private string GenerateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

    }
}
