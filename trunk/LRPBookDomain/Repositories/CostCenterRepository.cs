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
    public class CostCenterRepository : BaseRepository
    {

        #region Constructors
        public CostCenterRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create CostCenter and at least one CostCenter sign in.
        /// </summary>
        /// <param name="CostCenter"></param>
        /// <returns></returns>
        public CostCenter CreateCostCenter(CostCenter costcenter)
        {
            ctx.CostCenter.Add(costcenter);
            ctx.SaveChanges();
            return costcenter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CostCenter"></param>
        /// <returns></returns>
        public CostCenter CreateCostCenter(string CostCenter)
        {
            CostCenter newCostCenter = new CostCenter();
            newCostCenter.Name = CostCenter;
            return CreateCostCenter(newCostCenter);
        }
        #endregion

        #region Delete Methods
        public bool DeleteCostCenter(int id)
        {
            var CostCenter = ctx.CostCenter.Where(x => x.ID == id).First();
            ctx.CostCenter.Remove(CostCenter);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public CostCenter UpdateCostCenter(CostCenter CostCenter)
        {
            var c = ctx.CostCenter.Where(x => x.ID == CostCenter.ID).First();

            if (c != null)
            {
                c.Name = CostCenter.Name;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public CostCenter GetCostCenter(int id)
        {
            return ctx.CostCenter.Where(x => x.ID == id).First();
        }

        public CostCenter GetCostCenter(string name)
        {
            return ctx.CostCenter.Where(x => x.Name == name).First();
        }


        #endregion

        #region API Methods

        public List<CostCenter> GetCostCenters()
        {
            return (from p in ctx.CostCenter
                    orderby p.Name
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CostCenterList"></param>
        public void SaveCostCenters(List<CostCenter> CostCenters)
        {
            List<CostCenter> updateCostCenters = new List<CostCenter>();
            List<CostCenter> newCostCenters = new List<CostCenter>();
            List<int> updateIds = new List<int>();
            List<CostCenter> deleteCostCenters = new List<CostCenter>();

            if (CostCenters != null)
            {

                updateCostCenters = (from m in CostCenters where m.ID > 0 select m).ToList();
                newCostCenters = (from m in CostCenters where m.ID == 0 select m).ToList();
                updateIds = (from m in CostCenters where m.ID > 0 select m.ID).ToList();
            }

            deleteCostCenters = (from m in ctx.CostCenter
                                 where !updateIds.Contains(m.ID)
                              select m).ToList();

            //Delete CostCenter
            foreach (CostCenter CostCenter in deleteCostCenters)
            {
                ctx.CostCenter.Remove(CostCenter);
            }


            //Update CostCenters
            foreach (CostCenter CostCenter in updateCostCenters)
            {
                ctx.CostCenter.Attach(CostCenter);
                ctx.Entry(CostCenter).State = EntityState.Modified;
            }

            //Insert new CostCenters
            foreach (CostCenter CostCenter in newCostCenters)
            {
                ctx.CostCenter.Add(CostCenter);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}
