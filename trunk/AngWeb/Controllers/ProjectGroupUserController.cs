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
    public class ProjectGroupUserController : ApiController
    {
        public IHttpActionResult Get()
        {
            ProjectGroupUserRepository projectGroupUserRepository = new ProjectGroupUserRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            List<ProjectGroupUser> projectGroupUserList = projectGroupUserRepository.GetProjectGroupUsers();
            return Json(new { ProjectGroupUsers = projectGroupUserList });
        }

        // POST: api/ProjectGroupUser
        public IHttpActionResult Post([FromBody]List<ProjectGroupUser> projectGroupUsers)
        {
            ProjectGroupUserRepository projectGroupUserRepository = new ProjectGroupUserRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            projectGroupUserRepository.SaveProjectGroupUsers(projectGroupUsers);
            return Json(new { count = projectGroupUsers.Count.ToString() });
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
