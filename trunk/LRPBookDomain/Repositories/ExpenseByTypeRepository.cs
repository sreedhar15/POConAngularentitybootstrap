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
    public class ExpenseByTypeRepository : BaseRepository
    {
        #region Constructors
        public ExpenseByTypeRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create ExpenseByType and at least one ExpenseByType sign in.
        /// </summary>
        /// <param name="ExpenseByType"></param>
        /// <returns></returns>
        public ExpenseByType CreateExpenseByType(ExpenseByType ExpenseByType)
        {
            ctx.ExpenseByType.Add(ExpenseByType);
            ctx.SaveChanges();
            return ExpenseByType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExpenseByType"></param>
        /// <returns></returns>
        public ExpenseByType CreateExpenseByType(int employeeRoleTypeID, int employeeRoleID)
        {
            ExpenseByType newExpenseByType = new ExpenseByType();
            newExpenseByType.ExpenseTypeID = employeeRoleTypeID;
            newExpenseByType.ExpenseID = employeeRoleID;
            return CreateExpenseByType(newExpenseByType);
        }
        #endregion

        #region Delete Methods
        public bool DeleteExpenseByType(int id)
        {
            var ExpenseByType = ctx.ExpenseByType.Where(x => x.ID == id).First();
            ctx.ExpenseByType.Remove(ExpenseByType);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public ExpenseByType UpdateExpenseByType(ExpenseByType ExpenseByType)
        {
            var c = ctx.ExpenseByType.Where(x => x.ID == ExpenseByType.ID).First();

            if (c != null)
            {
                c.ExpenseTypeID = ExpenseByType.ExpenseTypeID;
                c.ExpenseID = ExpenseByType.ExpenseID;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public ExpenseByType GetExpenseByType(int id)
        {
            return ctx.ExpenseByType.Where(x => x.ID == id).First();
        }

        #endregion

        #region API Methods

        public List<ExpenseByType> GetExpenseByTypes()
        {
            return (from p in ctx.ExpenseByType
                    orderby p.ID
                    select p).ToList();
        }


        public List<ExpenseByType> GetExpenseByTypes(int expenseTypeID)
        {
            return (from p in ctx.ExpenseByType
                    where p.ExpenseTypeID == expenseTypeID
                    orderby p.ID
                    select p).ToList();
        }

        public List<BaseList> GetExpenseByTypesList(int expenseTypeID)
        {
            return (from p in ctx.ExpenseByType
                    join q in ctx.Expense on p.ExpenseID equals q.ID
                    where p.ExpenseTypeID == expenseTypeID
                    orderby p.ID
                    select new BaseList
                    {
                        ID = p.ID,
                        Name = q.Name
                    }).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExpenseByTypeList"></param>
        public void SaveExpenseByTypes(List<ExpenseByType> ExpenseByTypes, int expenseTypeID)
        {
            List<ExpenseByType> updateExpenseByTypes = new List<ExpenseByType>();
            List<ExpenseByType> newExpenseByTypes = new List<ExpenseByType>();
            List<int> updateIds = new List<int>();
            List<ExpenseByType> deleteExpenseByTypes = new List<ExpenseByType>();

            if (ExpenseByTypes != null)
            {
                updateExpenseByTypes = (from m in ExpenseByTypes where m.ID > 0 select m).ToList();
                newExpenseByTypes = (from m in ExpenseByTypes where m.ID == 0 select m).ToList();
                // by filter.
                updateIds = (from m in ExpenseByTypes where m.ExpenseTypeID == expenseTypeID && m.ID > 0 select m.ID).ToList();
            }

            deleteExpenseByTypes = (from m in ctx.ExpenseByType
                                    where m.ExpenseTypeID == expenseTypeID &&  !updateIds.Contains(m.ID)
                                    select m).ToList();

            //Delete ExpenseByType
            foreach (ExpenseByType ExpenseByType in deleteExpenseByTypes)
            {
                ctx.ExpenseByType.Remove(ExpenseByType);
            }


            //Update ExpenseByTypes
            foreach (ExpenseByType ExpenseByType in updateExpenseByTypes)
            {
                ctx.ExpenseByType.Attach(ExpenseByType);
                ctx.Entry(ExpenseByType).State = EntityState.Modified;
            }

            //Insert new ExpenseByTypes
            foreach (ExpenseByType ExpenseByType in newExpenseByTypes)
            {
                ExpenseByType.ExpenseTypeID = expenseTypeID;
                ctx.ExpenseByType.Add(ExpenseByType);
            }

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}