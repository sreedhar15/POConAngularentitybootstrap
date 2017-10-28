using AngWeb.Attributes;
using LRPBookDomain.Entities;
using LRPBookDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AngWeb.Controllers
{
    [AuthorizeUser(AccessLevel = AccessLevel.Admin)]
    public class ProjectGroupController : ApiController
    {
        // GET: api/ProjectGroup
        public IHttpActionResult Get()
        {
            ProjectGroupRepository projectGroupRepository = new ProjectGroupRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            List<ProjectGroup> projectGroups = projectGroupRepository.GetProjectGroups();
            return Json(new { ProjectGroups = projectGroups });
        }

        // GET: api/ProjectGroup/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ProjectGroup
        public IHttpActionResult Post([FromBody]List<ProjectGroup> projectGroups)
        {
            ProjectGroupRepository projectGroupRepository = new ProjectGroupRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            projectGroupRepository.SaveProjectGroups(projectGroups);
            return Json(new { count = projectGroups.Count.ToString() });
        }

        // PUT: api/ProjectGroup/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProjectGroup/5
        public void Delete(int id)
        {
        }
    }
}
