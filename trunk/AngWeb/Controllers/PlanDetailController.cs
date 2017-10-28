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
    /// A classs for PlanDetail Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class PlanDetailController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get PlanDetails
        /// </summary>
        /// <returns></returns>
        // GET: api/PlanDetail
        public IHttpActionResult Get(int planID)
        {

            PlanDetailRepository PlanDetailRepository = new PlanDetailRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<PlanDetail> PlanDetailList = PlanDetailRepository.GetPlanDetails(planID);

            return Json(new { PlanDetails = PlanDetailList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save PlanDetails
        /// </summary>
        /// <param name="PlanDetailList"></param>
        /// <returns></returns>
        // POST: api/PlanDetail
        public IHttpActionResult Post([FromBody]List<PlanDetail> PlanDetailList)
        {
            PlanDetailRepository PlanDetailRepository = new PlanDetailRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            PlanDetailRepository.SavePlanDetails(PlanDetailList);

            return Json(new { count = PlanDetailList.Count.ToString() });
        }
        #endregion

    }
}
