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
    [AuthorizeUser(AccessLevel = AccessLevel.User)]
    public class PlanGroupController : ApiController
    {
        public IHttpActionResult Get()
        {
            PlanGroupRepository planGroupRepository = new PlanGroupRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<PlanGroup> planGroupList = planGroupRepository.GetPlanGroups();

            return Json(new { planGroups = planGroupList });
        }

        // POST: api/Plan
        public IHttpActionResult Post([FromBody]List<PlanGroup> planGroupList)
        {
            PlanGroupRepository planGroupRepository = new PlanGroupRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            planGroupRepository.SavePlanGroups(planGroupList);

            return Json(new { count = planGroupList.Count.ToString() });
        }

        // PUT: api/Plan/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Plan/5
        public void Delete(int id)
        {
        }
    }
}