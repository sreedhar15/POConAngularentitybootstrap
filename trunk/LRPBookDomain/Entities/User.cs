using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LRPBookDomain.Entities
{
    public class User : Auditable
    {
        [Key]
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Phone { get; set; }
    }
}
