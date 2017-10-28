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
    public class PlanRepository : BaseRepository
    {
        #region Constructors
        public PlanRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create Plan and at least one Plan sign in.
        /// </summary>
        /// <param name="Plan"></param>
        /// <returns></returns>
        public Plan CreatePlan(Plan Plan)
        {
            ctx.Plan.Add(Plan);
            ctx.SaveChanges();
            return Plan;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Plan"></param>
        /// <returns></returns>
        public Plan CreatePlan(string Plan)
        {
            Plan newPlan = new Plan();
            newPlan.Name = Plan;
            return CreatePlan(newPlan);
        }
        #endregion

        #region Delete Methods
        public bool DeletePlan(int id)
        {
            var Plan = ctx.Plan.Where(x => x.ID == id).First();
            ctx.Plan.Remove(Plan);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public Plan UpdatePlan(Plan Plan)
        {
            var c = ctx.Plan.Where(x => x.ID == Plan.ID).First();

            if (c != null)
            {
                c.Name = Plan.Name;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public Plan GetPlan(int id)
        {
            return ctx.Plan.Where(x => x.ID == id).First();
        }

        public Plan GetPlan(string name)
        {
            return ctx.Plan.Where(x => x.Name == name).First();
        }


        #endregion

        #region API Methods

        public List<Plan> GetPlans()
        {
            return (from p in ctx.Plan
                    orderby p.Name
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlanList"></param>
        public void SavePlans(List<Plan> Plans)
        {
            List<Plan> updatePlans = new List<Plan>();
            List<Plan> newPlans = new List<Plan>();
            List<int> updateIds = new List<int>();
            List<Plan> deletePlans = new List<Plan>();

            if (Plans != null)
            {

                updatePlans = (from m in Plans where m.ID > 0 select m).ToList();
                newPlans = (from m in Plans where m.ID == 0 select m).ToList();
                updateIds = (from m in Plans where m.ID > 0 select m.ID).ToList();
            }

            deletePlans = (from m in ctx.Plan
                           where !updateIds.Contains(m.ID)
                           select m).ToList();

            //Delete Plan
            foreach (Plan Plan in deletePlans)
            {
                ctx.Plan.Remove(Plan);
            }


            //Update Plans
            foreach (Plan Plan in updatePlans)
            {
                ctx.Plan.Attach(Plan);
                ctx.Entry(Plan).State = EntityState.Modified;
            }

            //Insert new Plans
            foreach (Plan Plan in newPlans)
            {
                ctx.Plan.Add(Plan);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}