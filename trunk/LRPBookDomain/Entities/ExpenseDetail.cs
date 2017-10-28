using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LRPBookDomain.Entities
{

    public class ExpenseDetail : Auditable
    {
        [Key]
        public int ID { get; set; }

        public int ProjectID { get; set; }

        public int ExpenseTypeID { get; set; }

        public int ExpenseID { get; set; }

        public int CountryID { get; set; }

        public string Comment { get; set; }

        public int Year { get; set; }

        public decimal? Month01 { get; set; }

        public decimal? Month02 { get; set; }

        public decimal? Month03 { get; set; }

        public decimal? Month04 { get; set; }

        public decimal? Month05 { get; set; }

        public decimal? Month06 { get; set; }

        public decimal? Month07 { get; set; }

        public decimal? Month08 { get; set; }

        public decimal? Month09 { get; set; }

        public decimal? Month10 { get; set; }

        public decimal? Month11 { get; set; }

        public decimal? Month12 { get; set; }

    }
}
