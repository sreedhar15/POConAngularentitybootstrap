using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LRPBookDomain.Entities
{
    /// <summary>
    /// A class for employee role type
    /// </summary>
    public class EmployeeRoleType
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
