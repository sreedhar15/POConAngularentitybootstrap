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
    public class CountryController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get Countrys
        /// </summary>
        /// <returns></returns>
        // GET: api/Country
        public IHttpActionResult Get()
        {
            CountryRepository CountryRepository = new CountryRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<Country> CountryList = CountryRepository.GetCountrys();

            return Json(new { Countrys = CountryList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save Countrys
        /// </summary>
        /// <param name="CountryList"></param>
        /// <returns></returns>
        // POST: api/Country
        public IHttpActionResult Post([FromBody]List<Country> CountryList)
        {
            CountryRepository CountryRepository = new CountryRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            CountryRepository.SaveCountrys(CountryList);

            return Json(new { count = CountryList.Count.ToString() });
        }
        #endregion

    }
}
