using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFDAL.Models
{
    public class Service
    {
        public int ID { get; set; }
        public int PrivilegeID { get; set; }
        public string Name { get; set; }


        public virtual Privilege Privilege { get; set; }
    }
}
