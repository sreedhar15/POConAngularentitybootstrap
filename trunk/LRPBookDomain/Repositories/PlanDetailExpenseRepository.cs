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
    public class PlanDetailExpenseRepository : BaseRepository
    {
        #region Constructors
        public PlanDetailExpenseRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create PlanDetailExpense and at least one PlanDetailExpense sign in.
        /// </summary>
        /// <param name="PlanDetailExpense"></param>
        /// <returns></returns>
        public PlanDetailExpense CreatePlanDetailExpense(PlanDetailExpense PlanDetailExpense)
        {
            ctx.PlanDetailExpense.Add(PlanDetailExpense);
            ctx.SaveChanges();
            return PlanDetailExpense;
        }
        #endregion

        #region Delete Methods
        public bool DeletePlanDetailExpense(int id)
        {
            var PlanDetailExpense = ctx.PlanDetailExpense.Where(x => x.ID == id).First();
            ctx.PlanDetailExpense.Remove(PlanDetailExpense);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public PlanDetailExpense UpdatePlanDetailExpense(PlanDetailExpense PlanDetailExpense)
        {
            var c = ctx.PlanDetailExpense.Where(x => x.ID == PlanDetailExpense.ID).First();

            if (c != null)
            {
                c.PlanDetailID = PlanDetailExpense.PlanDetailID;
                c.ProjectID = PlanDetailExpense.ProjectID;
                c.ExpenseByTypeID = PlanDetailExpense.ExpenseByTypeID;
                c.Month1 = PlanDetailExpense.Month1;
                c.Month2 = PlanDetailExpense.Month2;
                c.Month3 = PlanDetailExpense.Month3;
                c.Month4 = PlanDetailExpense.Month4;
                c.Month5 = PlanDetailExpense.Month5;
                c.Month6 = PlanDetailExpense.Month6;
                c.Month7 = PlanDetailExpense.Month7;
                c.Month8 = PlanDetailExpense.Month8;
                c.Month9 = PlanDetailExpense.Month9;
                c.Month10 = PlanDetailExpense.Month10;
                c.Month11 = PlanDetailExpense.Month11;
                c.Month12 = PlanDetailExpense.Month12;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public PlanDetailExpense GetPlanDetailExpense(int id)
        {
            return ctx.PlanDetailExpense.Where(x => x.ID == id).First();
        }

        #endregion

        #region API Methods

        public List<PlanDetailExpense> GetPlanDetailExpenses(int planDetailID, int projectID, int ExpenseTypeID)
        {
            return (from p in ctx.PlanDetailExpense
                    where p.PlanDetailID == planDetailID && p.ProjectID == projectID && p.ExpenseTypeID == ExpenseTypeID
                    orderby p.ID
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlanDetailExpenseList"></param>
        public void SavePlanDetailExpenses(List<PlanDetailExpense> PlanDetailExpenses, int planDetailID, int projectID, int ExpenseTypeID)
        {
            List<PlanDetailExpense> updatePlanDetailExpenses = new List<PlanDetailExpense>();
            List<PlanDetailExpense> newPlanDetailExpenses = new List<PlanDetailExpense>();
            List<int> updateIds = new List<int>();
            List<PlanDetailExpense> deletePlanDetailExpenses = new List<PlanDetailExpense>();

            if (PlanDetailExpenses != null)
            {

                updatePlanDetailExpenses = (from m in PlanDetailExpenses where m.ID > 0 select m).ToList();
                newPlanDetailExpenses = (from m in PlanDetailExpenses where m.ID == 0 select m).ToList();
                //by filter.
                updateIds = (from m in PlanDetailExpenses where m.PlanDetailID == planDetailID && m.ProjectID == projectID && m.ExpenseTypeID == ExpenseTypeID && m.ID > 0 select m.ID).ToList();
            }

            //by filter.
            deletePlanDetailExpenses = (from m in ctx.PlanDetailExpense
                                        where m.PlanDetailID == planDetailID && m.ProjectID == projectID && m.ExpenseTypeID == ExpenseTypeID && !updateIds.Contains(m.ID)
                                        select m).ToList();

            //Delete PlanDetailExpense
            foreach (PlanDetailExpense PlanDetailExpense in deletePlanDetailExpenses)
            {
                ctx.PlanDetailExpense.Remove(PlanDetailExpense);
            }


            //Update PlanDetailExpenses
            foreach (PlanDetailExpense PlanDetailExpense in updatePlanDetailExpenses)
            {
                ctx.PlanDetailExpense.Attach(PlanDetailExpense);
                ctx.Entry(PlanDetailExpense).State = EntityState.Modified;
            }

            //Insert new PlanDetailExpenses
            foreach (PlanDetailExpense PlanDetailExpense in newPlanDetailExpenses)
            {
                PlanDetailExpense.PlanDetailID = planDetailID;
                PlanDetailExpense.ProjectID = projectID;
                //PlanDetailExpense.ExpenseByTypeID = ExpenseTypeID;
                PlanDetailExpense.ExpenseTypeID = ExpenseTypeID;

                ctx.PlanDetailExpense.Add(PlanDetailExpense);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}