using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LRPBookDomain.Entities;
using LRPBookTypes.DTO;
using LRPBookTypes.List;

namespace LRPBookDomain.Repositories
{
  public  class PLLineRepository : BaseRepository
    {

        #region Constructors
        public PLLineRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods 

        #region Create Methods
        /// <summary>
        /// Create PLLine.
        /// </summary>
        /// <param name="PLLine"></param>
        /// <returns></returns>
        public PLLine CreatePLLine(PLLine PLLine)
        {
            ctx.PLLine.Add(PLLine);
            ctx.SaveChanges();
            return PLLine;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PLLine"></param>
        /// <returns></returns>
        public PLLine CreatePLLine(string PLLine)
        {
            PLLine newPLLine = new PLLine();
            newPLLine.Name = PLLine;
            return CreatePLLine(newPLLine);
        }


        #endregion

        #region Delete Methods
        public bool DeletePLLine(int id)
        {
            var PLLine = ctx.PLLine.Where(x => x.ID == id).First();
            ctx.PLLine.Remove(PLLine);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public PLLine UpdatePLLine(PLLine PLLine)
        {
            var c = ctx.PLLine.Where(x => x.ID == PLLine.ID).First();

            if (c != null)
            {
                c.Name = PLLine.Name;
                ctx.SaveChanges();
            }
            return c;
        }

        #endregion


        #region Get Methods

        public PLLine GetPLLine(int id)
        {
            return ctx.PLLine.Where(x => x.ID == id).First();
        }

        public PLLine PLLine(string name)
        {
            return ctx.PLLine.Where(x => x.Name == name).First();
        }

        #endregion

        #region API Methods

        public List<PLLine> GetPLLines()
        {
            return (from p in ctx.PLLine
                    orderby p.Name
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PLLineList"></param>
        public void SavePLLines(List<PLLine> PLLines)
        {
            List<PLLine> updatePlans = new List<PLLine>();
            List<PLLine> newPlans = new List<PLLine>();
            List<int> updateIds = new List<int>();
            List<PLLine> deletePlans = new List<PLLine>();

            if (PLLines != null)
            {

                updatePlans = (from m in PLLines where m.ID > 0 select m).ToList();
                newPlans = (from m in PLLines where m.ID == 0 select m).ToList();
                updateIds = (from m in PLLines where m.ID > 0 select m.ID).ToList();
            }

            deletePlans = (from m in ctx.PLLine
                           where !updateIds.Contains(m.ID)
                           select m).ToList();

            //Delete Plan
            foreach (PLLine PLLine in deletePlans)
            {
                ctx.PLLine.Remove(PLLine);
            }


            //Update PLLine
            foreach (PLLine PLLine in updatePlans)
            {
                ctx.PLLine.Attach(PLLine);
                ctx.Entry(PLLine).State = EntityState.Modified;
            }

            //Insert new PLLines
            foreach (PLLine PLLine in newPlans)
            {
                ctx.PLLine.Add(PLLine);
            }

            ctx.SaveChanges();

        }


        #endregion


        #endregion

        
    }
}
