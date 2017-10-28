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
    public class PlanDetailHeadCountRepository : BaseRepository
    {
        #region Constructors
        public PlanDetailHeadCountRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create PlanDetailHeadCount and at least one PlanDetailHeadCount sign in.
        /// </summary>
        /// <param name="PlanDetailHeadCount"></param>
        /// <returns></returns>
        public PlanDetailHeadCount CreatePlanDetailHeadCount(PlanDetailHeadCount PlanDetailHeadCount)
        {
            ctx.PlanDetailHeadCount.Add(PlanDetailHeadCount);
            ctx.SaveChanges();
            return PlanDetailHeadCount;
        }
        #endregion

        #region Delete Methods
        public bool DeletePlanDetailHeadCount(int id)
        {
            var PlanDetailHeadCount = ctx.PlanDetailHeadCount.Where(x => x.ID == id).First();
            ctx.PlanDetailHeadCount.Remove(PlanDetailHeadCount);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public PlanDetailHeadCount UpdatePlanDetailHeadCount(PlanDetailHeadCount PlanDetailHeadCount)
        {
            var c = ctx.PlanDetailHeadCount.Where(x => x.ID == PlanDetailHeadCount.ID).First();

            if (c != null)
            {
                c.PlanDetailID = PlanDetailHeadCount.PlanDetailID;
                c.ProjectID = PlanDetailHeadCount.ProjectID;
                c.EmployeeRoleByTypeID = PlanDetailHeadCount.EmployeeRoleByTypeID;
                c.Month1 = PlanDetailHeadCount.Month1;
                c.Month2 = PlanDetailHeadCount.Month2;
                c.Month3 = PlanDetailHeadCount.Month3;
                c.Month4 = PlanDetailHeadCount.Month4;
                c.Month5 = PlanDetailHeadCount.Month5;
                c.Month6 = PlanDetailHeadCount.Month6;
                c.Month7 = PlanDetailHeadCount.Month7;
                c.Month8 = PlanDetailHeadCount.Month8;
                c.Month9 = PlanDetailHeadCount.Month9;
                c.Month10 = PlanDetailHeadCount.Month10;
                c.Month11 = PlanDetailHeadCount.Month11;
                c.Month12 = PlanDetailHeadCount.Month12;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public PlanDetailHeadCount GetPlanDetailHeadCount(int id)
        {
            return ctx.PlanDetailHeadCount.Where(x => x.ID == id).First();
        }

        #endregion

        #region API Methods

        public List<PlanDetailHeadCount> GetPlanDetailHeadCounts(int planDetailID, int projectID, int employeeRoleTypeID)
        {
            return (from p in ctx.PlanDetailHeadCount
                    where p.PlanDetailID==planDetailID && p.ProjectID == projectID && p.EmployeeRoleTypeID == employeeRoleTypeID
                    orderby p.ID
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlanDetailHeadCountList"></param>
        public void SavePlanDetailHeadCounts(List<PlanDetailHeadCount> PlanDetailHeadCounts, int planDetailID, int projectID, int employeeRoleTypeID)
        {
            List<PlanDetailHeadCount> updatePlanDetailHeadCounts = new List<PlanDetailHeadCount>();
            List<PlanDetailHeadCount> newPlanDetailHeadCounts = new List<PlanDetailHeadCount>();
            List<int> updateIds = new List<int>();
            List<PlanDetailHeadCount> deletePlanDetailHeadCounts = new List<PlanDetailHeadCount>();

            if (PlanDetailHeadCounts != null)
            {

                updatePlanDetailHeadCounts = (from m in PlanDetailHeadCounts where m.ID > 0 select m).ToList();
                newPlanDetailHeadCounts = (from m in PlanDetailHeadCounts where m.ID == 0 select m).ToList();
                //by filter.
                updateIds = (from m in PlanDetailHeadCounts where m.PlanDetailID==planDetailID && m.ProjectID==projectID && m.EmployeeRoleTypeID==employeeRoleTypeID &&  m.ID > 0 select m.ID).ToList();
            }

            //by filter.
            deletePlanDetailHeadCounts = (from m in ctx.PlanDetailHeadCount
                                          where m.PlanDetailID == planDetailID &&  m.ProjectID == projectID && m.EmployeeRoleTypeID == employeeRoleTypeID && !updateIds.Contains(m.ID)
                                          select m).ToList();

            //Delete PlanDetailHeadCount
            foreach (PlanDetailHeadCount PlanDetailHeadCount in deletePlanDetailHeadCounts)
            {
                ctx.PlanDetailHeadCount.Remove(PlanDetailHeadCount);
            }


            //Update PlanDetailHeadCounts
            foreach (PlanDetailHeadCount PlanDetailHeadCount in updatePlanDetailHeadCounts)
            {
                ctx.PlanDetailHeadCount.Attach(PlanDetailHeadCount);
                ctx.Entry(PlanDetailHeadCount).State = EntityState.Modified;
            }

            //Insert new PlanDetailHeadCounts
            foreach (PlanDetailHeadCount PlanDetailHeadCount in newPlanDetailHeadCounts)
            {
                PlanDetailHeadCount.PlanDetailID = planDetailID;
                PlanDetailHeadCount.ProjectID = projectID;
                //PlanDetailHeadCount.EmployeeRoleByTypeID = employeeRoleTypeID;
                PlanDetailHeadCount.EmployeeRoleTypeID = employeeRoleTypeID;

                ctx.PlanDetailHeadCount.Add(PlanDetailHeadCount);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}