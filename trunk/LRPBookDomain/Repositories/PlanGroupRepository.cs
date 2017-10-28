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
    public class PlanGroupRepository : BaseRepository
    {

        #region Constructors
        public PlanGroupRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods 

        #region Create Methods
        /// <summary>
        /// Create Plan and at least one Plan sign in.
        /// </summary>
        /// <param name="PlanGroup"></param>
        /// <returns></returns>
        public PlanGroup CreatePlanGroup(PlanGroup PlanGroup)
        {
            ctx.PlanGroup.Add(PlanGroup);
            ctx.SaveChanges();
            return PlanGroup;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlanGroup"></param>
        /// <returns></returns>
        public PlanGroup CreatePlanGroup(string PlanGroup)
        {
            PlanGroup newPlanGroup = new PlanGroup();
            newPlanGroup.Name = PlanGroup;
            return CreatePlanGroup(newPlanGroup);
        }
        #endregion

        #region Delete Methods
        public bool DeletePlanGroup(int id)
        {
            var PlanGroup = ctx.PlanGroup.Where(x => x.ID == id).First();
            ctx.PlanGroup.Remove(PlanGroup);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public PlanGroup UpdatePlanGroup(PlanGroup PlanGroup)
        {
            var c = ctx.PlanGroup.Where(x => x.ID == PlanGroup.ID).First();

            if (c != null)
            {
                c.Name = PlanGroup.Name;
                ctx.SaveChanges();
            }
            return c;
        }

        #endregion

        #region Get Methods

        public PlanGroup GetPlanGroup(int id)
        {
            return ctx.PlanGroup.Where(x => x.ID == id).First();
        }

        public PlanGroup PlanGroup(string name)
        {
            return ctx.PlanGroup.Where(x => x.Name == name).First();
        }

        #endregion

        #region API Methods

        public List<PlanGroup> GetPlanGroups()
        {
            return (from p in ctx.PlanGroup
                    orderby p.Name
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlanGroupList"></param>
        public void SavePlanGroups(List<PlanGroup> PlanGgroups)
        {
            List<PlanGroup> updatePlans = new List<PlanGroup>();
            List<PlanGroup> newPlans = new List<PlanGroup>();
            List<int> updateIds = new List<int>();
            List<PlanGroup> deletePlans = new List<PlanGroup>();

            if (PlanGgroups != null)
            {

                updatePlans = (from m in PlanGgroups where m.ID > 0 select m).ToList();
                newPlans = (from m in PlanGgroups where m.ID == 0 select m).ToList();
                updateIds = (from m in PlanGgroups where m.ID > 0 select m.ID).ToList();
            }

            deletePlans = (from m in ctx.PlanGroup
                           where !updateIds.Contains(m.ID)
                           select m).ToList();

            //Delete Plan
            foreach (PlanGroup PlanGroup in deletePlans)
            {
                ctx.PlanGroup.Remove(PlanGroup);
            }


            //Update Plans
            foreach (PlanGroup PlanGroup in updatePlans)
            {
                ctx.PlanGroup.Attach(PlanGroup);
                ctx.Entry(PlanGroup).State = EntityState.Modified;
            }

            //Insert new Plans
            foreach (PlanGroup PlanGroup in newPlans)
            {
                ctx.PlanGroup.Add(PlanGroup);
            }

            ctx.SaveChanges();

        }


        #endregion

        #endregion
    }
}
