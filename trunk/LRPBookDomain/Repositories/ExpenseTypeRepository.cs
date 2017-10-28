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
    public class ExpenseTypeRepository : BaseRepository
    {
        #region Constructors
        public ExpenseTypeRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create ExpenseType and at least one ExpenseType sign in.
        /// </summary>
        /// <param name="ExpenseType"></param>
        /// <returns></returns>
        public ExpenseType CreateExpenseType(ExpenseType ExpenseType)
        {
            ctx.ExpenseType.Add(ExpenseType);
            ctx.SaveChanges();
            return ExpenseType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExpenseType"></param>
        /// <returns></returns>
        public ExpenseType CreateExpenseType(string ExpenseType)
        {
            ExpenseType newExpenseType = new ExpenseType();
            newExpenseType.Name = ExpenseType;
            return CreateExpenseType(newExpenseType);
        }
        #endregion

        #region Delete Methods
        public bool DeleteExpenseType(int id)
        {
            var ExpenseType = ctx.ExpenseType.Where(x => x.ID == id).First();
            ctx.ExpenseType.Remove(ExpenseType);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public ExpenseType UpdateExpenseType(ExpenseType ExpenseType)
        {
            var c = ctx.ExpenseType.Where(x => x.ID == ExpenseType.ID).First();

            if (c != null)
            {
                c.Name = ExpenseType.Name;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public ExpenseType GetExpenseType(int id)
        {
            return ctx.ExpenseType.Where(x => x.ID == id).First();
        }

        public ExpenseType GetExpenseType(string name)
        {
            return ctx.ExpenseType.Where(x => x.Name == name).First();
        }


        #endregion

        #region API Methods

        public List<ExpenseType> GetExpenseTypes()
        {
            return (from p in ctx.ExpenseType
                    orderby p.Name
                    select p).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExpenseTypeList"></param>
        public void SaveExpenseTypes(List<ExpenseType> ExpenseTypes)
        {
            List<ExpenseType> updateExpenseTypes = new List<ExpenseType>();
            List<ExpenseType> newExpenseTypes = new List<ExpenseType>();
            List<int> updateIds = new List<int>();
            List<ExpenseType> deleteExpenseTypes = new List<ExpenseType>();

            if (ExpenseTypes != null)
            {

                updateExpenseTypes = (from m in ExpenseTypes where m.ID > 0 select m).ToList();
                newExpenseTypes = (from m in ExpenseTypes where m.ID == 0 select m).ToList();
                updateIds = (from m in ExpenseTypes where m.ID > 0 select m.ID).ToList();
            }

            deleteExpenseTypes = (from m in ctx.ExpenseType
                                  where !updateIds.Contains(m.ID)
                                  select m).ToList();

            //Delete ExpenseType
            foreach (ExpenseType ExpenseType in deleteExpenseTypes)
            {
                ctx.ExpenseType.Remove(ExpenseType);
            }


            //Update ExpenseTypes
            foreach (ExpenseType ExpenseType in updateExpenseTypes)
            {
                ctx.ExpenseType.Attach(ExpenseType);
                ctx.Entry(ExpenseType).State = EntityState.Modified;
            }

            //Insert new ExpenseTypes
            foreach (ExpenseType ExpenseType in newExpenseTypes)
            {
                ctx.ExpenseType.Add(ExpenseType);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}