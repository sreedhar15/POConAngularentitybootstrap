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
    public class SecurityRoleController : ApiController
    {
        // GET: api/SecurityRole
        public IHttpActionResult Get()
        {
            SecurityRoleRepository securityRoleRepository = new SecurityRoleRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            List<SecurityRole> securityRoleList = securityRoleRepository.GetSecurityRoles();
            return Json(new { SecurityRoles = securityRoleList });
        }

        // GET: api/SecurityRole/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SecurityRole
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SecurityRole/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SecurityRole/5
        public void Delete(int id)
        {
        }
    }
}
