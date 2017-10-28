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
    /// A classs for EmployeeRole Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class EmployeeRoleController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get EmployeeRoles
        /// </summary>
        /// <returns></returns>
        // GET: api/EmployeeRole
        public IHttpActionResult Get()
        {
            EmployeeRoleRepository EmployeeRoleRepository = new EmployeeRoleRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<EmployeeRole> EmployeeRoleList = EmployeeRoleRepository.GetEmployeeRoles();

            return Json(new { EmployeeRoles = EmployeeRoleList });
        }

        /// <summary>
        /// Get EmployeeRoles
        /// </summary>
        /// <returns></returns>
        // GET: api/EmployeeRole
        public IHttpActionResult Get(string filter)
        {
            EmployeeRoleRepository EmployeeRoleRepository = new EmployeeRoleRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<EmployeeRole> EmployeeRoleList = EmployeeRoleRepository.GetEmployeeRolesByFilter(filter);

            return Json(new { EmployeeRoles = EmployeeRoleList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save EmployeeRoles
        /// </summary>
        /// <param name="EmployeeRoleList"></param>
        /// <returns></returns>
        // POST: api/EmployeeRole
        public IHttpActionResult Post([FromBody]List<EmployeeRole> EmployeeRoleList, string filter)
        {
            EmployeeRoleRepository EmployeeRoleRepository = new EmployeeRoleRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            EmployeeRoleRepository.SaveEmployeeRoles(EmployeeRoleList, filter);

            return Json(new { count = EmployeeRoleList.Count.ToString() });
        }
        #endregion

    }
}
