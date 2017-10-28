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
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class PLLineController : ApiController
    {

        public IHttpActionResult Get()
        {
            PLLineRepository pllineRepository = new PLLineRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<PLLine> PLLineList = pllineRepository.GetPLLines();

            return Json(new { pllines = PLLineList });
        }

        public IHttpActionResult Post([FromBody]List<PLLine> pllineList)
        {
            PLLineRepository pllineRepository = new PLLineRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            pllineRepository.SavePLLines(pllineList);

            return Json(new { count = pllineList.Count.ToString() });
        }

        // PUT: api/PLLine/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PLLine/5
        public void Delete(int id)
        {
        }


    }
}