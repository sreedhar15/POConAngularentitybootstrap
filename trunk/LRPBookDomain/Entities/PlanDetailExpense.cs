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
    public class PlanDetailExpense : Auditable
    {
        [Key]
        public int ID { get; set; }
        public int PlanDetailID { get; set; }
        public int ProjectID { get; set; }
        public int ExpenseByTypeID { get; set; }

        public int ExpenseTypeID { get; set; }
        public decimal Month1 { get; set; }
        public decimal Month2 { get; set; }
        public decimal Month3 { get; set; }
        public decimal Month4 { get; set; }
        public decimal Month5 { get; set; }
        public decimal Month6 { get; set; }
        public decimal Month7 { get; set; }
        public decimal Month8 { get; set; }
        public decimal Month9 { get; set; }
        public decimal Month10 { get; set; }
        public decimal Month11 { get; set; }
        public decimal Month12 { get; set; }
    }
}
