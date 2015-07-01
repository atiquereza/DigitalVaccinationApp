using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace System.Web.Http
{
    public class CustomAuthorize : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(
               System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            HttpSessionStateBase Session = (HttpSessionStateBase)HttpContext.Current.Session["SessionBackup"];

            if (Session != null)
            {
                //Dictionary<string, string> sessionData = SessionHandler.GetSessionData(Session);
                //if (!Authentication.Authenticate(sessionData, Session))
                //{
                //    ViewResult result = new ViewResult();
                //    result.ViewName = "Error";
                //    result.ViewData["Message"] = "You dont have privilege for this action.!";
                //    filterContext.Result = result;
                //}
            }
            else
            {
                //actionContext.Response=null;
               
                HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}

////http://www.asp.net/web-api/overview/security/authentication-filters