using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngWeb.Attributes;
using LRPBookDomain.Repositories;
using LRPBookDomain.Entities;
using LRPBookTypes.List;

namespace AngWeb.Controllers
{
    /// <summary>
    /// A classs for ExpenseByType Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class ExpenseByTypeController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get ExpenseByTypes
        /// </summary>
        /// <returns></returns>
        // GET: api/ExpenseByType
        public IHttpActionResult Get()
        {
            ExpenseByTypeRepository ExpenseByTypeRepository = new ExpenseByTypeRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<ExpenseByType> ExpenseByTypeList = ExpenseByTypeRepository.GetExpenseByTypes();

            return Json(new { ExpenseByTypes = ExpenseByTypeList });
        }
        /// <summary>
        /// Get ExpenseByTypes
        /// </summary>
        /// <returns></returns>
        // GET: api/ExpenseByType
        public IHttpActionResult Get(int expenseTypeID)
        {
            ExpenseByTypeRepository ExpenseByTypeRepository = new ExpenseByTypeRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<ExpenseByType> ExpenseByTypeList = ExpenseByTypeRepository.GetExpenseByTypes(expenseTypeID);

            return Json(new { ExpenseByTypes = ExpenseByTypeList });
        }

        /// <summary>
        /// Get ExpenseByTypes
        /// </summary>
        /// <returns></returns>
        // GET: api/ExpenseByType
        public IHttpActionResult Get(bool list, int expenseTypeID)
        {
            ExpenseByTypeRepository ExpenseByTypeRepository = new ExpenseByTypeRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<BaseList> ExpenseByTypeList = ExpenseByTypeRepository.GetExpenseByTypesList(expenseTypeID);

            return Json(new { ExpenseByTypesList = ExpenseByTypeList });
        }

       
        #endregion

        #region Post Methods
        /// <summary>
        /// Save ExpenseByTypes
        /// </summary>
        /// <param name="ExpenseByTypeList"></param>
        /// <returns></returns>
        // POST: api/ExpenseByType
        public IHttpActionResult Post([FromBody]List<ExpenseByType> ExpenseByTypeList, int expenseTypeID)
        {
            ExpenseByTypeRepository ExpenseByTypeRepository = new ExpenseByTypeRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            ExpenseByTypeRepository.SaveExpenseByTypes(ExpenseByTypeList, expenseTypeID);

            return Json(new { count = ExpenseByTypeList.Count.ToString() });
        }
        #endregion

    }
}
