using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFDAL.Models
{
    public class Accreditation
    {
        public int ID { get; set; }
        public int PrivilegeID { get; set; }
        public int UserID { get; set; }

        public virtual Privilege Privilege { get; set; }
        public virtual User User { get; set; }
    }
}
