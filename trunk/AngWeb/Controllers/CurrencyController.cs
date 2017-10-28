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
    /// A classs for Currency Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class CurrencyController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get Currencys
        /// </summary>
        /// <returns></returns>
        // GET: api/Currency
        public IHttpActionResult Get()
        {
            CurrencyRepository CurrencyRepository = new CurrencyRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<Currency> CurrencyList = CurrencyRepository.GetCurrencys();

            return Json(new { Currencys = CurrencyList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save Currencys
        /// </summary>
        /// <param name="CurrencyList"></param>
        /// <returns></returns>
        // POST: api/Currency
        public IHttpActionResult Post([FromBody]List<Currency> CurrencyList)
        {
            CurrencyRepository CurrencyRepository = new CurrencyRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            CurrencyRepository.SaveCurrencys(CurrencyList);

            return Json(new { count = CurrencyList.Count.ToString() });
        }
        #endregion

    }
}
