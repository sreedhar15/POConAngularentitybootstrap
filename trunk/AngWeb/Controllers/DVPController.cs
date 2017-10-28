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
    /// A classs for DVP Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class DVPController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get DVPs
        /// </summary>
        /// <returns></returns>
        // GET: api/DVP
        public IHttpActionResult Get()
        {
            DVPRepository DVPRepository = new DVPRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<DVP> DVPList = DVPRepository.GetDVPs();

            return Json(new { DVPs = DVPList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save DVPs
        /// </summary>
        /// <param name="DVPList"></param>
        /// <returns></returns>
        // POST: api/DVP
        public IHttpActionResult Post([FromBody]List<DVP> DVPList)
        {
            DVPRepository DVPRepository = new DVPRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            DVPRepository.SaveDVPs(DVPList);

            return Json(new { count = DVPList.Count.ToString() });
        }
        #endregion

    }
}
