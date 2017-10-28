using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngWeb.Attributes;
using LRPBookDomain.Repositories;
using LRPBookDomain.Entities;
using LRPBookTypes.List;


namespace AngWeb.Controllers
{
    /// <summary>
    /// A classs for EmployeeRoleByType Controller
    /// </summary>
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class EmployeeRoleByTypeController : ApiController
    {
        #region Get Methods
        /// <summary>
        /// Get EmployeeRoleByTypes
        /// </summary>
        /// <returns></returns>
        // GET: api/EmployeeRoleByType
        public IHttpActionResult Get()
        {
            EmployeeRoleByTypeRepository EmployeeRoleByTypeRepository = new EmployeeRoleByTypeRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<EmployeeRoleByType> EmployeeRoleByTypeList = EmployeeRoleByTypeRepository.GetEmployeeRoleByTypes();

            return Json(new { EmployeeRoleByTypes = EmployeeRoleByTypeList });
        }

        /// <summary>
        /// Get EmployeeRoleByTypes
        /// </summary>
        /// <returns></returns>
        // GET: api/EmployeeRoleByType
        public IHttpActionResult Get(int employeeRoleTypeID)
        {
            EmployeeRoleByTypeRepository EmployeeRoleByTypeRepository = new EmployeeRoleByTypeRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<EmployeeRoleByType> EmployeeRoleByTypeList = EmployeeRoleByTypeRepository.GetEmployeeRoleByTypes(employeeRoleTypeID);

            return Json(new { EmployeeRoleByTypes = EmployeeRoleByTypeList });
        }

        /// <summary>
        /// Get EmployeeRoleByTypes
        /// </summary>
        /// <returns></returns>
        // GET: api/EmployeeRoleByType
        public IHttpActionResult Get(bool list, int employeeRoleTypeID)
        {
            EmployeeRoleByTypeRepository EmployeeRoleByTypeRepository = new EmployeeRoleByTypeRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<BaseList> EmployeeRoleByTypeList = EmployeeRoleByTypeRepository.GetEmployeeRoleByTypesList(employeeRoleTypeID);

            return Json(new { EmployeeRoleByTypesList = EmployeeRoleByTypeList });
        }
        #endregion

        #region Post Methods
        /// <summary>
        /// Save EmployeeRoleByTypes
        /// </summary>
        /// <param name="EmployeeRoleByTypeList"></param>
        /// <returns></returns>
        // POST: api/EmployeeRoleByTypeC:\Work\LRP\AngWeb\Controllers\EmployeeRoleByTypeController.cs
        public IHttpActionResult Post([FromBody]List<EmployeeRoleByType> EmployeeRoleByTypeList, int employeeRoleTypeID)
        {
            EmployeeRoleByTypeRepository EmployeeRoleByTypeRepository = new EmployeeRoleByTypeRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            EmployeeRoleByTypeRepository.SaveEmployeeRoleByTypes(EmployeeRoleByTypeList, employeeRoleTypeID);

            return Json(new { count = EmployeeRoleByTypeList.Count.ToString() });
        }
        #endregion

    }
}
