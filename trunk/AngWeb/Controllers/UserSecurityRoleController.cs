using AngWeb.Attributes;
using LRPBookDomain.Entities;
using LRPBookDomain.Repositories;
using LRPBookTypes.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AngWeb.Controllers
{
    [AuthorizeUser(AccessLevel = AccessLevel.Admin)]
    public class UserSecurityRoleController : ApiController
    {
        public IHttpActionResult Get()
        {
            UserSecurityRoleRepository userSecurityRoleRepository = new UserSecurityRoleRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            List<UserSecurityRole> userSecurityRoleList = userSecurityRoleRepository.GetUserSecurityRoles();
            return Json(new { UserSecurityRoles = userSecurityRoleList });
        }

        // POST: api/UserSecurityRole
        public IHttpActionResult Post([FromBody]List<UserSecurityRole> userSecurityRoles)
        {
            UserSecurityRoleRepository userSecurityRoleRepository = new UserSecurityRoleRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            userSecurityRoleRepository.SaveUserSecurityRoles(userSecurityRoles);
            return Json(new { count = userSecurityRoles.Count.ToString() });
        }

        // PUT: api/UserSecurityRole/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserSecurityRole/5
        public void Delete(int id)
        {
        }
    }
}