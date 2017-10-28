using AngWeb.Attributes;
using LRPBookDomain.Entities;
using LRPBookDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngWeb.Controllers
{
    [AuthorizeUser(AccessLevel = AccessLevel.Admin)]
    public class CustomProjectGroupController : ApiController
    {
        public IHttpActionResult Get()
        {
            CustomProjectGroupRepository customProjectGroupRepository = new CustomProjectGroupRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            List<CustomProjectGroup> customProjectGroupList = customProjectGroupRepository.GetCustomProjectGroups();
            return Json(new { CustomProjectGroups = customProjectGroupList });
        }

        // POST: api/ProjectGroupUser
        public IHttpActionResult Post([FromBody]List<CustomProjectGroup> customProjectGroups, int customGroupId, int projectGroupId)
        {
            CustomProjectGroupRepository customProjectGroupRepository = new CustomProjectGroupRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            customProjectGroupRepository.SaveCustomProjectGroups(customProjectGroups, customGroupId, projectGroupId);
            return Json(new { count = customProjectGroups.Count.ToString() });
        }
        public IHttpActionResult Get(int customGroupId, int projectGroupId)
        {
            CustomProjectGroupRepository customProjectGroupRepository = new CustomProjectGroupRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            List<CustomProjectGroup> customProjectGroupList = customProjectGroupRepository.GetCustomProjectGroups(customGroupId, projectGroupId);
            return Json(new { CustomProjectGroups = customProjectGroupList });
        }
        // PUT: api/ProjectGroupUser/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProjectGroupUser/5
        public void Delete(int id)
        {
        }
    }
}
