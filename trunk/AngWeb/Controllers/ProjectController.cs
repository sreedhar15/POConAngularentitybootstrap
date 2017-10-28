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

    [AuthorizeUser(AccessLevel=AccessLevel.Admin)]
    public class ProjectController : ApiController
    {

        public IHttpActionResult Get()
        {
            ProjectRepository projectRepository = new ProjectRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<Project> projects = projectRepository.GetProjects();

            return Json(new { projects = projects });
        }

        // GET: api/Project
        public IHttpActionResult Get(string filter)
        {
            ProjectRepository projectRepository = new ProjectRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            /*
            List<Project> projectList = new List<Project>();

            Project project = new Project { ID  = 1, Name = "Test1" };
            projectList.Add(project);

            project = new Project { ID = 2, Name = "Test2"};
            projectList.Add(project);
            */

            List<Project> projectList = projectRepository.GetProjectsByFilter(filter);

            return Json(new { projects = projectList });
        }


        // GET: api/Project/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Project
        public IHttpActionResult Post([FromBody]List<Project> projectList, string filter)
        {
            ProjectRepository projectRepository = new ProjectRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            projectRepository.SaveProjects(projectList, filter);

            return Json(new { count = projectList.Count.ToString() });
        }

        // PUT: api/Project/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Project/5
        public void Delete(int id)
        {
        }


    }
}
