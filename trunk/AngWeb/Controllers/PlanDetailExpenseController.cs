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
    /// A classs for PlanDetailExpense Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class PlanDetailExpenseController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get PlanDetailExpenses
        /// </summary>
        /// <returns></returns>
        // GET: api/PlanDetailExpense
        public IHttpActionResult Get(int planDetailID, int projectID, int expenseTypeID)
        {
            PlanDetailExpenseRepository PlanDetailExpenseRepository = new PlanDetailExpenseRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<PlanDetailExpense> PlanDetailExpenseList = PlanDetailExpenseRepository.GetPlanDetailExpenses(planDetailID, projectID, expenseTypeID);

            return Json(new { PlanDetailExpenses = PlanDetailExpenseList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save PlanDetailExpenses
        /// </summary>
        /// <param name="PlanDetailExpenseList"></param>
        /// <returns></returns>
        // POST: api/PlanDetailExpense
        public IHttpActionResult Post([FromBody]List<PlanDetailExpense> PlanDetailExpenseList, int planDetailID, int projectID, int expenseTypeID)
        {
            PlanDetailExpenseRepository PlanDetailExpenseRepository = new PlanDetailExpenseRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            PlanDetailExpenseRepository.SavePlanDetailExpenses(PlanDetailExpenseList, planDetailID, projectID, expenseTypeID);

            return Json(new { count = PlanDetailExpenseList.Count.ToString() });
        }
        #endregion

    }
}
