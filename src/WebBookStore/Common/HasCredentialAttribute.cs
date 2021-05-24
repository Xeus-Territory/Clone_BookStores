using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBookStore.Common
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string RoleID { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            var session = (UserLogin)HttpContext.Current.Session[CommonConstant.USER_SESSION];
            if (session == null)
            {
                return false;
            }
            List<string> privilegeLevels = this.GetCredentialByLoggedInUser(session.UserName);
            if (privilegeLevels == null)
            {
                return false;
            }
            else
            {
                if (privilegeLevels.Contains(this.RoleID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        private List<string> GetCredentialByLoggedInUser(string userName)
        {
            var credentials = (List<string>)HttpContext.Current.Session[CommonConstant.SESSION_CREDENTIALS];
            return credentials;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/404.cshtml"
            };
        }
    }
}