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
    public class CustomGroupController : ApiController
    {
        public IHttpActionResult Get()
        {
            CustomGroupRepository customGroupRepository = new CustomGroupRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<CustomGroup> customGroupList = customGroupRepository.GetCustomGroups();

            return Json(new { customGroups = customGroupList });
        }
       
        // POST: api/Plan
        public IHttpActionResult Post([FromBody]List<CustomGroup> customGroupList)
        {
            CustomGroupRepository customGroupRepository = new CustomGroupRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            customGroupRepository.SaveCustomGroups(customGroupList);

            return Json(new { count = customGroupList.Count.ToString() });
        }

        // PUT: api/Plan/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Plan/5
        public void Delete(int id)
        {
        }
    }
}