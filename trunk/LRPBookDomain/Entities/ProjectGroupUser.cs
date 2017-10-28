using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRPBookDomain.Entities
{
    public class ProjectGroupUser : Auditable
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }
        public int ProjectGroupID { get; set; }
        public int SecurityRoleID { get; set; }
    }
}
