using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFDAL.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required, StringLength(50)]
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public virtual ICollection<Privilege> Privileges { get; set; }

    }
}
