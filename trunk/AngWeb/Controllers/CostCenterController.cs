using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngWeb.Attributes;
using LRPBookDomain.Repositories;
using LRPBookDomain.Entities;
using LRPBookLibrary;

namespace AngWeb.Controllers
{
    /// <summary>
    ///  A classs for CostCenter Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class CostCenterController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get CostCenters
        /// </summary>
        /// <returns></returns>
        // GET: api/CostCenter
        public IHttpActionResult Get()
        {
            CostCenterRepository CostCenterRepository = new CostCenterRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<CostCenter> CostCenterList = CostCenterRepository.GetCostCenters();

            return Json(new { CostCenters = CostCenterList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save CostCenters
        /// </summary>
        /// <param name="CostCenterList"></param>
        /// <returns></returns>
        // POST: api/CostCenter
        public IHttpActionResult Post([FromBody]List<CostCenter> CostCenterList)
        {
            CostCenterRepository CostCenterRepository = new CostCenterRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            CostCenterRepository.SaveCostCenters(CostCenterList);

            return Json(new { count = CostCenterList.Count.ToString() });
        }
        #endregion

    }
}