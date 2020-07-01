using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFDAL.Models;

namespace WCFDAL
{
    public class BaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DAO>
    {
        protected override void Seed(DAO context)
        {
            var users = new List<User>
            {
                new User{Login="fantou", Password="root", Email="robin.maisano@gmail.com" },
                new User{Login="michel", Password="root", Email="michel@gmail.com" },
                new User{Login="tata", Password="toto", Email="tata@yahoo.com" }
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var privileges = new List<Privilege>
            {
                new Privilege{ ID=1, Name="admin" },
                new Privilege{ ID=2, Name="user" },
                new Privilege{ ID=3, Name="dumbUser" }
            };
            privileges.ForEach(p => context.Privileges.Add(p));
            context.SaveChanges();

            var services = new List<Service>
            {
                new Service{ Name="Register", PrivilegeID=3 },
                new Service{ Name="Login", PrivilegeID=3 },
                new Service{ Name="Decrypt", PrivilegeID=2 }
            };
            services.ForEach(s => context.Services.Add(s));
            context.SaveChanges();

            var accreditations = new List<Accreditation>
            {
                new Accreditation{ UserID=1, PrivilegeID=1 },
                new Accreditation{ UserID=1, PrivilegeID=2 },
                new Accreditation{ UserID=1, PrivilegeID=3 },
                new Accreditation{ UserID=2, PrivilegeID=2 },
                new Accreditation{ UserID=2, PrivilegeID=3 },
                new Accreditation{ UserID=3, PrivilegeID=3 }
            };
            accreditations.ForEach(a => context.Accreditations.Add(a));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
