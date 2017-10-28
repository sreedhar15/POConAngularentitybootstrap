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
    public class DVPRepository : BaseRepository
    {
        #region Constructors
        public DVPRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create DVP and at least one DVP sign in.
        /// </summary>
        /// <param name="DVP"></param>
        /// <returns></returns>
        public DVP CreateDVP(DVP DVP)
        {
            ctx.DVP.Add(DVP);
            ctx.SaveChanges();
            return DVP;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DVP"></param>
        /// <returns></returns>
        public DVP CreateDVP(string DVP)
        {
            DVP newDVP = new DVP();
            newDVP.Name = DVP;
            return CreateDVP(newDVP);
        }
        #endregion

        #region Delete Methods
        public bool DeleteDVP(int id)
        {
            var DVP = ctx.DVP.Where(x => x.ID == id).First();
            ctx.DVP.Remove(DVP);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public DVP UpdateDVP(DVP DVP)
        {
            var c = ctx.DVP.Where(x => x.ID == DVP.ID).First();

            if (c != null)
            {
                c.Name = DVP.Name;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public DVP GetDVP(int id)
        {
            return ctx.DVP.Where(x => x.ID == id).First();
        }

        public DVP GetDVP(string name)
        {
            return ctx.DVP.Where(x => x.Name == name).First();
        }


        #endregion

        #region API Methods

        public List<DVP> GetDVPs()
        {
            return (from p in ctx.DVP
                    orderby p.Name
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DVPList"></param>
        public void SaveDVPs(List<DVP> DVPs)
        {
            List<DVP> updateDVPs = new List<DVP>();
            List<DVP> newDVPs = new List<DVP>();
            List<int> updateIds = new List<int>();
            List<DVP> deleteDVPs = new List<DVP>();

            if (DVPs != null)
            {

                updateDVPs = (from m in DVPs where m.ID > 0 select m).ToList();
                newDVPs = (from m in DVPs where m.ID == 0 select m).ToList();
                updateIds = (from m in DVPs where m.ID > 0 select m.ID).ToList();
            }

            deleteDVPs = (from m in ctx.DVP
                          where !updateIds.Contains(m.ID)
                          select m).ToList();

            //Delete DVP
            foreach (DVP DVP in deleteDVPs)
            {
                ctx.DVP.Remove(DVP);
            }


            //Update DVPs
            foreach (DVP DVP in updateDVPs)
            {
                ctx.DVP.Attach(DVP);
                ctx.Entry(DVP).State = EntityState.Modified;
            }

            //Insert new DVPs
            foreach (DVP DVP in newDVPs)
            {
                ctx.DVP.Add(DVP);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}