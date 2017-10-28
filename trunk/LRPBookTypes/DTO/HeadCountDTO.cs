using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRPBookTypes.DTO
{
    public class HeadCountDTO 
    {
        public string EmployeeRole { get; set; }

        public string Incremental { get; set; }

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
    public class HeadCountDTOComparer : IEqualityComparer<HeadCountDTO>
    {
        public bool Equals(HeadCountDTO x, HeadCountDTO y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the base DTO ' properties are equal.
            return x.EmployeeRole == y.EmployeeRole;
        }
        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(HeadCountDTO baseDTO)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(baseDTO, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashName = baseDTO.EmployeeRole == null ? 0 : baseDTO.EmployeeRole.GetHashCode();

            //Calculate the hash code for the product.
            return hashName;
        }
    }
}
