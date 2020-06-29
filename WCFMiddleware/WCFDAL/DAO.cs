using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WCFDAL.Models;

namespace WCFDAL
{
    public class DAO : DbContext
    {
        public DAO() : base("WCF")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Privilege> Privileges { get; set; }
        public DbSet<Accreditation> Accreditations{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Entity<User>().HasIndex(u => u.Login ).IsUnique(true);
            Database.SetInitializer<DAO>(null);
            base.OnModelCreating(modelBuilder);
        }

        private static DAO _instance = null;
        private static readonly object _lock = new object();

        public static DAO Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DAO();
                    }
                    return _instance;
                }
            }
        }
    }
}
