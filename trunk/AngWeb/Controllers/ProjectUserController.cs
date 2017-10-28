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
    public class ProjectUserController : ApiController
    {
        public IHttpActionResult Get()
        {
            ProjectUserRepository projectUserRepository = new ProjectUserRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            List<ProjectUser> projectUserList = projectUserRepository.GetProjectUsers();
            return Json(new { ProjectUsers = projectUserList });
        }

        // POST: api/ProjectUser
        public IHttpActionResult Post([FromBody]List<ProjectUser> projectUsers)
        {
            ProjectUserRepository projectUserRepository = new ProjectUserRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            projectUserRepository.SaveProjectUsers(projectUsers);
            return Json(new { count = projectUsers.Count.ToString() });
        }

        // PUT: api/ProjectUser/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProjectUser/5
        public void Delete(int id)
        {
        }

    }
}
