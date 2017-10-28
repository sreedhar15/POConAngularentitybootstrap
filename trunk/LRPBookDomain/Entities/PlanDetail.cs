using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LRPBookDomain.Entities
{
    /// <summary>
    /// A class for plan detail
    /// </summary>
    public class PlanDetail : Auditable
    {
        [Key]
        public int ID { get; set; }
        public int PlanID { get; set; }
        public int Year { get; set; }
    }
}
