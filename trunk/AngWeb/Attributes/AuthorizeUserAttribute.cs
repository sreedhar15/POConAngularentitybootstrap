using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Web.Routing;
using LRPBookDomain.Repositories;
using LRPBookLibrary;

namespace AngWeb.Attributes
{
    public enum AccessLevel
    {
        SuperAdmin = 1,
        Admin = 2,
        User = 3
    }

    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public AccessLevel AccessLevel { get; set; }

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
           string identityName = actionContext.RequestContext.Principal.Identity.Name;
           
           string accessLevel = AccessLevel.ToString();

           UserRepository userRepository = new UserRepository(BaseRepository.SystemUserID);

           int userID = userRepository.GetUserIDInRole(SecurityHelper.GetUserName(identityName), accessLevel);

           if (userID>0)
           {
               actionContext.Request.Headers.Add("CurrentUserID", userID.ToString());

           }
           else {

               userID = userRepository.GetUserIDInRole(SecurityHelper.GetUserName(identityName), "Super Admin");

               if (userID > 0)
               {
                   actionContext.Request.Headers.Add("CurrentUserID", userID.ToString());
               }
               else
               {
                   HandleUnauthorizedRequest(actionContext);
               }
               
           }
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            /*var response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Redirect);
            response.Headers.Add("Location", "http://lrpError.com");
            actionContext.Response = response;*/
            throw new Exception("You are not authorized to access the page.");
        }
    }
}