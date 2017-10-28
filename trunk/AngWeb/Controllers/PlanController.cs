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
    public class PlanController : ApiController
    {
        public IHttpActionResult Get()
        {
            PlanRepository planRepository = new PlanRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<Plan> planList = planRepository.GetPlans();

            return Json(new { plans = planList });
        }

        // GET: api/Project/5
        public string Get(int id)
        {
            return "value";
        }


        // POST: api/Plan
        public IHttpActionResult Post([FromBody]List<Plan> planList)
        {
            PlanRepository planRepository = new PlanRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            planRepository.SavePlans(planList);

            return Json(new { count = planList.Count.ToString() });
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