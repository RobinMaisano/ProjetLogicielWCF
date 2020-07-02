namespace WCFDAL.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WCFDAL.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WCFDAL.DAO>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WCFDAL.DAO context)
        {
            //  This method will be called after migrating to the latest version.

            var users = new List<User>
            {
                new User{Login="fantou", Password="Cesi123!", Email="robin.maisano@gmail.com", Privileges = new List<Privilege>() },
                new User{Login="michel", Password="root", Email="michel@gmail.com", Privileges = new List<Privilege>() },
                new User{Login="tata", Password="toto", Email="tata@yahoo.com", Privileges = new List<Privilege>() }
            };


            var privileges = new List<Privilege>
            {
                new Privilege{ ID=1, Name="admin" },
                new Privilege{ ID=2, Name="user" },
                new Privilege{ ID=3, Name="dumbUser" }
            };

            var services = new List<Service>
            {
                new Service{ Name="Register", PrivilegeID=3 },
                new Service{ Name="Login", PrivilegeID=3 },
                new Service{ Name="Decrypt", PrivilegeID=2 },
                new Service{ Name="Decrypted", PrivilegeID=2 },
                new Service{ Name="IsDecrypted", PrivilegeID=2 }
            };

            var accreditations = new List<Accreditation>
            {
                new Accreditation{ UserID=1, PrivilegeID=1 },
                new Accreditation{ UserID=1, PrivilegeID=2 },
                new Accreditation{ UserID=1, PrivilegeID=3 },
                new Accreditation{ UserID=2, PrivilegeID=2 },
                new Accreditation{ UserID=2, PrivilegeID=3 },
                new Accreditation{ UserID=3, PrivilegeID=3 }
            };

            users[0].Privileges.Add(privileges[0]);
            users[0].Privileges.Add(privileges[1]);
            users[0].Privileges.Add(privileges[2]);

            users[1].Privileges.Add(privileges[1]);
            users[1].Privileges.Add(privileges[2]);

            users[2].Privileges.Add(privileges[2]);

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            //privileges.ForEach(p => context.Privileges.Add(p));
            //context.SaveChanges();

            services.ForEach(s => context.Services.Add(s));
            context.SaveChanges();

            accreditations.ForEach(a => context.Accreditations.Add(a));
            context.SaveChanges();

            base.Seed(context);

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
