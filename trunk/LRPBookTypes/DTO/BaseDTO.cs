using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRPBookTypes.DTO
{ 
    /// <summary>
    /// Base class data transfer objects
    /// </summary>
    public class BaseDTO
    { 
        public string KeyName { get; set; }
        public BaseDTO()
        {

        }
        public BaseDTO(string keyName)
        {
            KeyName = keyName;
        }

        public override string ToString()
        {
            return KeyName;
        }

    }

    /// <summary>
    /// Comparer for BaseDTO
    /// </summary>
    public class BaseDTOComparer : IEqualityComparer<BaseDTO>
    {
        public bool Equals(BaseDTO x, BaseDTO y)
        {
            
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the base DTO ' properties are equal.
            return  x.KeyName == y.KeyName;
        }
        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(BaseDTO baseDTO)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(baseDTO, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashName = baseDTO.KeyName == null ? 0 : baseDTO.KeyName.GetHashCode();

            //Calculate the hash code for the product.
            return hashName;
        }
    }
    
}

