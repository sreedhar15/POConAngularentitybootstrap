using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LRPBookLibrary;
using LRPBookDomain.Repositories;
using LRPBookDomain.Entities;
using AngWeb.Attributes;

namespace AngWeb.Controllers
{
    [AuthorizeUser(AccessLevel = AccessLevel.Admin)]
    public class UserController : ApiController
    {

        [NonAction]
        public IHttpActionResult CreateResult(bool success, string message)
        {
            return Json(new { success = success, message = message });
        }
        
        // GET: api/User

        public IHttpActionResult Get()
        {
            //IHttpActionResult ret = null;
            UserRepository userRepository = new UserRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));
            List<User> userList = userRepository.GetUsers();


            return Json(new { users = userList });
        }
        //public string Get()
        //{
        //    string userName = this.User.Identity.Name;

        //    return "ValidUser";
        //}

        // GET: api/User/5
        public IHttpActionResult Get(int userID)
        {

            UserRepository userRepository = new UserRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            User userList = userRepository.GetUser(userID);

            return Json(new { users = userList });
        }
        //GET: api/User

        public IHttpActionResult Get(string filter)
        {
            UserRepository userRepository = new UserRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            List<User> userList = userRepository.GetUsersByFilter(filter);

            return Json(new { users = userList });
        }

        public IHttpActionResult Post([FromBody]List<User> UserDetails, string filter)
        {
            UserRepository userRepository = new UserRepository(Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First()));

            userRepository.SaveUsers(UserDetails, filter);

            return Json(new { count = UserDetails.Count.ToString() });
        }
        // POST: api/User
        public IHttpActionResult Post([FromBody]string text, string credential)
        {
            bool valid=false;

            string identityName = User.Identity.Name;
            UserCredential userCredential = SecurityHelper.GetUserCredential(credential);
            UserCredential webCredential = SecurityHelper.GetUserCredential(identityName);

           if(userCredential.LoginName!=webCredential.LoginName) 
                valid=false;
           else if (SecurityHelper.IsValidLDAPUser(identityName, userCredential.Password))
                valid = true;

           if (valid)
           {
               int userID = Convert.ToInt32(Request.Headers.GetValues("CurrentUserID").First());
               UserSecurityRoleRepository usrRoleRepository=new UserSecurityRoleRepository(userID);
               return Json(new {message="ValidUser", userRoles = usrRoleRepository.GetUserSecurityRoles(userID) });
           }
           else {
               return Json(new {message="InvalidUser", userRoles = new List<int>() });
           }
            
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
