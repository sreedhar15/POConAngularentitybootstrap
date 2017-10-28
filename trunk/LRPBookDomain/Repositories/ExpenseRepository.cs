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
    public class ExpenseRepository : BaseRepository
    {
        #region Constructors
        public ExpenseRepository(int userID) : base(userID)
        {

        }
        #endregion

        #region Public Methods   

        #region Create Methods
        /// <summary>
        /// Create Expense and at least one Expense sign in.
        /// </summary>
        /// <param name="Expense"></param>
        /// <returns></returns>
        public Expense CreateExpense(Expense Expense)
        {
            ctx.Expense.Add(Expense);
            ctx.SaveChanges();
            return Expense;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Expense"></param>
        /// <returns></returns>
        public Expense CreateExpense(string Expense)
        {
            Expense newExpense = new Expense();
            newExpense.Name = Expense;
            return CreateExpense(newExpense);
        }
        #endregion

        #region Delete Methods
        public bool DeleteExpense(int id)
        {
            var Expense = ctx.Expense.Where(x => x.ID == id).First();
            ctx.Expense.Remove(Expense);
            if (ctx.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update Methods
        public Expense UpdateExpense(Expense Expense)
        {
            var c = ctx.Expense.Where(x => x.ID == Expense.ID).First();

            if (c != null)
            {
                c.Name = Expense.Name;
                ctx.SaveChanges();
            }
            return c;
        }


        #endregion

        #region Get Methods

        public Expense GetExpense(int id)
        {
            return ctx.Expense.Where(x => x.ID == id).First();
        }

        public Expense GetExpense(string name)
        {
            return ctx.Expense.Where(x => x.Name == name).First();
        }


        #endregion

        #region API Methods

        public List<Expense> GetExpenses()
        {
            return (from p in ctx.Expense
                    orderby p.Name
                    select p).ToList();
        }

        public List<Expense> GetExpensesByFilter(string filter)
        {
            string[] splitData = filter.Split(new char[] { ',' });

            return (from p in ctx.Expense
                    where splitData.Any(c => p.Name.StartsWith(c))
                    orderby p.Name
                    select p).ToList();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExpenseList"></param>
        public void SaveExpenses(List<Expense> Expenses, string filter)
        {
            string[] splitData = filter.Split(new char[] { ',' });


            List<Expense> updateExpenses = new List<Expense>();
            List<Expense> newExpenses = new List<Expense>();
            List<int> updateIds = new List<int>();
            List<Expense> deleteExpenses = new List<Expense>();

            if (Expenses != null)
            {
                updateExpenses = (from m in Expenses where m.ID > 0 select m).ToList();
                newExpenses = (from m in Expenses where m.ID == 0 select m).ToList();
                // by filter.
                updateIds = (from m in Expenses where splitData.Any(c => m.Name.StartsWith(c)) && m.ID > 0 select m.ID).ToList();
            }

            //by filter.
            deleteExpenses = (from m in ctx.Expense
                              where splitData.Any(c => m.Name.StartsWith(c)) && !updateIds.Contains(m.ID)
                              select m).ToList();

            //Delete Expense
            foreach (Expense Expense in deleteExpenses)
            {
                ctx.Expense.Remove(Expense);
            }

            //Insert new Expenses
            foreach (Expense Expense in newExpenses)
            {
                ctx.Expense.Add(Expense);
            }

            //Update Expenses
            foreach (Expense Expense in updateExpenses)
            {
                ctx.Expense.Attach(Expense);
                ctx.Entry(Expense).State = EntityState.Modified;
            }

           

            ctx.SaveChanges();

        }
        #endregion

        #endregion

    }
}