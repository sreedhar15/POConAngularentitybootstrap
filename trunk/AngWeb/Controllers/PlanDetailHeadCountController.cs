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
    /// A classs for PlanDetailHeadCount Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class PlanDetailHeadCountController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get PlanDetailHeadCounts
        /// </summary>
        /// <returns></returns>
        // GET: api/PlanDetailHeadCount
        public IHttpActionResult Get(int planDetailID, int projectID, int employeeRoleTypeID)
        {
            PlanDetailHeadCountRepository PlanDetailHeadCountRepository = new PlanDetailHeadCountRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<PlanDetailHeadCount> PlanDetailHeadCountList = PlanDetailHeadCountRepository.GetPlanDetailHeadCounts(planDetailID, projectID, employeeRoleTypeID);

            return Json(new { PlanDetailHeadCounts = PlanDetailHeadCountList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save PlanDetailHeadCounts
        /// </summary>
        /// <param name="PlanDetailHeadCountList"></param>
        /// <returns></returns>
        // POST: api/PlanDetailHeadCount
        public IHttpActionResult Post([FromBody]List<PlanDetailHeadCount> PlanDetailHeadCountList, int planDetailID, int projectID, int employeeRoleTypeID)
        {
            PlanDetailHeadCountRepository PlanDetailHeadCountRepository = new PlanDetailHeadCountRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            PlanDetailHeadCountRepository.SavePlanDetailHeadCounts(PlanDetailHeadCountList, planDetailID, projectID, employeeRoleTypeID);

            return Json(new { count = PlanDetailHeadCountList.Count.ToString() });
        }
        #endregion

    }
}
