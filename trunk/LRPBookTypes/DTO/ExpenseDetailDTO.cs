using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRPBookTypes.DTO
{
    public class ExpenseDetailDTO
    {
        public string Expense { get; set; }

        public string Country { get; set; }

        public string Comment { get; set; }

        public string Month01 { get; set; }

        public string Month02 { get; set; }

        public string Month03 { get; set; }

        public string Month04 { get; set; }

        public string Month05 { get; set; }

        public string Month06 { get; set; }

        public string Month07 { get; set; }

        public string Month08 { get; set; }

        public string Month09 { get; set; }

        public string Month10 { get; set; }

        public string Month11 { get; set; }

        public string Month12 { get; set; }

    }

    /// <summary>
    /// Comparer for HeadCountDTO
    /// </summary>
    public class ExpenseDetailDTOComparer : IEqualityComparer<ExpenseDetailDTO>
    {
        public bool Equals(ExpenseDetailDTO x, ExpenseDetailDTO y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the base DTO ' properties are equal.
            return x.Expense == y.Expense;
        }
        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(ExpenseDetailDTO baseDTO)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(baseDTO, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashName = baseDTO.Expense == null ? 0 : baseDTO.Expense.GetHashCode();

            //Calculate the hash code for the product.
            return hashName;
        }
    }
}
