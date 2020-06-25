using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WCFDAL.Models
{
    public class Privilege
    {
        public int ID { get; set; }
        public string Name { get; set; }


        public virtual ICollection<User> Users { get; set; }


        public Privilege()
        {
            this.Users = new HashSet<User>();
        }

    }
}