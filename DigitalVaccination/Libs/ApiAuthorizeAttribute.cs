using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;


namespace DigitalVaccination.Libs
{
    public class ApiAuthorize : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(
               System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            HttpSessionStateBase Session = (HttpSessionStateBase)HttpContext.Current.Session["SessionBackup"];

            if (Session == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                return;
            }

            if (Session != null && Session.Count == 0)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                return;
            }

            if (Session != null)
            {
                Dictionary<string, string> session = SessionHandler.GetSessionData(Session);
                if(Authentication.ApiAuthenticate(session,Session,actionContext))
                {
                    return;
                }

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                return;
            }
        }



    }
}
