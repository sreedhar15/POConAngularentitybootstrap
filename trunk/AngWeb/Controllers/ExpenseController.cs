using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngWeb.Attributes;
using LRPBookDomain.Repositories;
using LRPBookDomain.Entities;

namespace AngWeb.Controllers
{
    /// <summary>
    /// A classs for Expense Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class ExpenseController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get Expenses
        /// </summary>
        /// <returns></returns>
        // GET: api/Expense
        public IHttpActionResult Get()
        {
            ExpenseRepository ExpenseRepository = new ExpenseRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<Expense> ExpenseList = ExpenseRepository.GetExpenses();

            return Json(new { Expenses = ExpenseList });
        }

        /// <summary>
        /// Get Expenses
        /// </summary>
        /// <returns></returns>
        // GET: api/Expense
        public IHttpActionResult Get(string filter)
        {
            ExpenseRepository ExpenseRepository = new ExpenseRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<Expense> ExpenseList = ExpenseRepository.GetExpensesByFilter(filter);

            return Json(new { Expenses = ExpenseList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save Expenses
        /// </summary>
        /// <param name="ExpenseList"></param>
        /// <returns></returns>
        // POST: api/Expense
        public IHttpActionResult Post([FromBody]List<Expense> ExpenseList, string filter)
        {
            ExpenseRepository ExpenseRepository = new ExpenseRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            ExpenseRepository.SaveExpenses(ExpenseList, filter);

            return Json(new { count = ExpenseList.Count.ToString() });
        }
        #endregion

    }
}
