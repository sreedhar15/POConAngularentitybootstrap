using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LRPBookDomain.Concrete;

namespace LRPBookDomain.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        protected LRPDbContext ctx;

        public static int SystemUserID
        {
            get
            {
                return 65536;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static int CurrentUserID(string loginName)
        {
            return new LRPDbContext(SystemUserID).User.Where(x => x.LoginName == loginName).Select(x => x.ID).First<int>();
        }


        public BaseRepository(int contextUserID)
        {
            ctx = new LRPDbContext(contextUserID);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
