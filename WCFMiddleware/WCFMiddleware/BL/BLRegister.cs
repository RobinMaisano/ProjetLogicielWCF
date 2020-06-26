using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFDAL;
using WCFDAL.Models;

namespace WCFMiddleware
{
    class BLRegister
    {
        public bool IsLoginAlreadyUsed(string login)
        {
            throw new NotImplementedException();
        }

        public bool Register(string login, string password, string[] groups)
        {
            DAO DAO = DAO.Instance;
            //TODO: handle groups

            //DAO.Users.Add(user);
            //DAO.SaveChanges();
            //Console.WriteLine(DAO.Users.First().Login);
            //string name = DAO.Privileges.ToList().First().Name;
            //Console.WriteLine(name);

            Console.WriteLine("List of all users:");

            foreach (var item in DAO.Users.ToList())
            {
                string _login = item.Login;
                Console.WriteLine("\n" + item.Login);
                item.Privileges.ToList().ForEach(p => Console.WriteLine(p.Name));

            }

            //return DAO.AddUser(login, password);
            //return DAO.Users.First().Login;
            return true;
        }
    }
}
