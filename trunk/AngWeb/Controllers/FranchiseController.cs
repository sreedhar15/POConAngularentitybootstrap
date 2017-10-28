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
    /// A classs for Country Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class FranchiseController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get Countrys
        /// </summary>
        /// <returns></returns>
        // GET: api/Country
        public IHttpActionResult Get()
        {
            FranchiseRepository FranchiseRepository = new FranchiseRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<Franchise> FranchiseList = FranchiseRepository.GetFranchises();

            return Json(new { Franchises = FranchiseList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save Countrys
        /// </summary>
        /// <param name="CountryList"></param>
        /// <returns></returns>
        // POST: api/Country
        public IHttpActionResult Post([FromBody]List<Franchise> FranchiseList)
        {
            FranchiseRepository FranchiseRepository = new FranchiseRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            FranchiseRepository.SaveFranchises(FranchiseList);

            return Json(new { count = FranchiseList.Count.ToString() });
        }
        #endregion

    }
}
