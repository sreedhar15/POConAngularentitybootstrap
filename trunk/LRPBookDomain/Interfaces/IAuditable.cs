using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRPBookDomain.Interfaces
{
    interface IAuditable
    {
        void SetCreated(int userID);
        void SetUpdated(int userID);
       
    }
}
