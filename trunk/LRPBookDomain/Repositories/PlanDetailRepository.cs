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
    public class PlanDetailRepository : BaseRepository
    {
        #region Constructors
        public PlanDetailRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create PlanDetail and at least one PlanDetail sign in.
        /// </summary>
        /// <param name="PlanDetail"></param>
        /// <returns></returns>
        public PlanDetail CreatePlanDetail(PlanDetail PlanDetail)
        {
            ctx.PlanDetail.Add(PlanDetail);
            ctx.SaveChanges();
            return PlanDetail;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlanDetail"></param>
        /// <returns></returns>
        public PlanDetail CreatePlanDetail(int planID, int year)
        {
            PlanDetail newPlanDetail = new PlanDetail();
            newPlanDetail.PlanID = planID;
            newPlanDetail.Year = year;
            return CreatePlanDetail(newPlanDetail);
        }
        #endregion

        #region Delete Methods
        public bool DeletePlanDetail(int id)
        {
            var PlanDetail = ctx.PlanDetail.Where(x => x.ID == id).First();
            ctx.PlanDetail.Remove(PlanDetail);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public PlanDetail UpdatePlanDetail(PlanDetail PlanDetail)
        {
            var c = ctx.PlanDetail.Where(x => x.ID == PlanDetail.ID).First();

            if (c != null)
            {
                c.PlanID = PlanDetail.PlanID;
                c.Year = PlanDetail.Year;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public PlanDetail GetPlanDetail(int id)
        {
            return ctx.PlanDetail.Where(x => x.ID == id).First();
        }

        public PlanDetail GetPlanDetail(int planID, int year)
        {
            return ctx.PlanDetail.Where(x => x.PlanID == planID && x.Year == year).First();
        }


        #endregion

        #region API Methods

        public List<PlanDetail> GetPlanDetails(int planID)
        {
            return (from p in ctx.PlanDetail
                    where p.PlanID == planID
                    orderby p.PlanID, p.Year
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlanDetailList"></param>
        public void SavePlanDetails(List<PlanDetail> PlanDetails)
        {
            List<PlanDetail> updatePlanDetails = new List<PlanDetail>();
            List<PlanDetail> newPlanDetails = new List<PlanDetail>();
            List<int> updateIds = new List<int>();
            List<PlanDetail> deletePlanDetails = new List<PlanDetail>();

            if (PlanDetails != null)
            {

                updatePlanDetails = (from m in PlanDetails where m.ID > 0 select m).ToList();
                newPlanDetails = (from m in PlanDetails where m.ID == 0 select m).ToList();
                updateIds = (from m in PlanDetails where m.ID > 0 select m.ID).ToList();
            }

            deletePlanDetails = (from m in ctx.PlanDetail
                                 where !updateIds.Contains(m.ID)
                                 select m).ToList();

            //Delete PlanDetail
            foreach (PlanDetail PlanDetail in deletePlanDetails)
            {
                ctx.PlanDetail.Remove(PlanDetail);
            }


            //Update PlanDetails
            foreach (PlanDetail PlanDetail in updatePlanDetails)
            {
                ctx.PlanDetail.Attach(PlanDetail);
                ctx.Entry(PlanDetail).State = EntityState.Modified;
            }

            //Insert new PlanDetails
            foreach (PlanDetail PlanDetail in newPlanDetails)
            {
                ctx.PlanDetail.Add(PlanDetail);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}