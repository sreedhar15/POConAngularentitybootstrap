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
    /// A classs for EmployeeRoleType Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class EmployeeRoleTypeController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get EmployeeRoleTypes
        /// </summary>
        /// <returns></returns>
        // GET: api/EmployeeRoleType
        public IHttpActionResult Get()
        {
            EmployeeRoleTypeRepository EmployeeRoleTypeRepository = new EmployeeRoleTypeRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<EmployeeRoleType> EmployeeRoleTypeList = EmployeeRoleTypeRepository.GetEmployeeRoleTypes();

            return Json(new { EmployeeRoleTypes = EmployeeRoleTypeList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save EmployeeRoleTypes
        /// </summary>
        /// <param name="EmployeeRoleTypeList"></param>
        /// <returns></returns>
        // POST: api/EmployeeRoleType
        public IHttpActionResult Post([FromBody]List<EmployeeRoleType> EmployeeRoleTypeList)
        {
            EmployeeRoleTypeRepository EmployeeRoleTypeRepository = new EmployeeRoleTypeRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            EmployeeRoleTypeRepository.SaveEmployeeRoleTypes(EmployeeRoleTypeList);

            return Json(new { count = EmployeeRoleTypeList.Count.ToString() });
        }
        #endregion

    }
}
