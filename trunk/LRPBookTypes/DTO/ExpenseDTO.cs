using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRPBookTypes.DTO
{
    public class ExpenseDTO : BaseDTO
    {
        public ExpenseDTO()
        {

        }
        public ExpenseDTO(string keyName) : base(keyName)
        {
        }
    }
}
