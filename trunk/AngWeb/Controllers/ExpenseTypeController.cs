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
    /// A classs for ExpenseType Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class ExpenseTypeController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get ExpenseTypes
        /// </summary>
        /// <returns></returns>
        // GET: api/ExpenseType
        public IHttpActionResult Get()
        {
            ExpenseTypeRepository ExpenseTypeRepository = new ExpenseTypeRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<ExpenseType> ExpenseTypeList = ExpenseTypeRepository.GetExpenseTypes();

            return Json(new { ExpenseTypes = ExpenseTypeList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save ExpenseTypes
        /// </summary>
        /// <param name="ExpenseTypeList"></param>
        /// <returns></returns>
        // POST: api/ExpenseType
        public IHttpActionResult Post([FromBody]List<ExpenseType> ExpenseTypeList)
        {
            ExpenseTypeRepository ExpenseTypeRepository = new ExpenseTypeRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            ExpenseTypeRepository.SaveExpenseTypes(ExpenseTypeList);

            return Json(new { count = ExpenseTypeList.Count.ToString() });
        }
        #endregion

    }
}
