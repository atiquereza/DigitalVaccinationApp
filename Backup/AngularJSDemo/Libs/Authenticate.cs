using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DigitalVaccination.Controllers;

namespace DigitalVaccination.Libs
{
    public class AuthenticateAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {

            HttpSessionStateBase Session = (HttpSessionStateBase)HttpContext.Current.Session["SessionBackup"];

            if (Session == null)
            {   
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary { { "controller", "Account" }, { "action", "Index" } }); ;
                
                return;
            }

            if (Session != null && Session.Count == 0)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary { { "controller", "Account" }, { "action", "Index" } }); ;

                return;
            }



            if (Session != null)
            {
                Dictionary<string, string> sessionData = SessionHandler.GetSessionData(Session);
                if (!Authentication.Authenticate(sessionData, Session))
                {
                    ViewResult result = new ViewResult();
                    result.ViewName = "Error";
                    result.ViewData["Message"] = "You dont have privilege for this action.!";
                    filterContext.Result = result;
                }
            }
        }


    }
}