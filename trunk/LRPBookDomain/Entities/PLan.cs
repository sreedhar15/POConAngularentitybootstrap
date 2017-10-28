using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LRPBookDomain.Entities
{
    /// <summary>
    /// A class for plan
    /// </summary>
    public class Plan : Auditable
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        public int PlanGroupID { get; set; }
    }
}
