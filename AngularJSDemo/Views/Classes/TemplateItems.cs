using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngularJSDemo.Libs;

namespace AngularJSDemo.Views.Classes
{
    public static class TemplateItems
    {
        //public static string WelcomeMessage = "Welcome to O&M Supervision";


        public static string GetWelcomeStringView()
        {
            string WelcomeMessage = "";//;"Welcome to OMCB";
            string htmlData = string.Empty;

            htmlData += "<div class=\"center\"><a><h>" + WelcomeMessage;
            htmlData += "</h></a></div>";

            return htmlData;
        }
        public static string GetHeaderLogInView(HttpSessionStateBase Session)
        {
            string htmlData = string.Empty;
            if(Session.Count > 0)
            {
                Dictionary<string, string> sessionData = SessionHandler.GetSessionData(Session);
                htmlData += "<div style=\"text-align:right;\">";
                htmlData += "<a>Logged In as: " + sessionData["LoggedIn"] + "</a><br>";
                htmlData += "</div>";
            }

            return htmlData;
        }

        public static bool HasAdminView(HttpSessionStateBase Session)
        {
            if (Session.Count == 0 || Session == null)
            {
                return false;
            }

            Dictionary<string, string> sessionData = SessionHandler.GetSessionData(Session);
            if (sessionData["RoleName"] == "ViewUser")
            {
                return false;
            }

            return true;
        }


        public static bool HasUserView(HttpSessionStateBase Session)
        {
            if (Session.Count == 0 || Session == null)
            {
                return false;
            }

            Dictionary<string, string> sessionData = SessionHandler.GetSessionData(Session);
            if (sessionData["RoleName"] == "ViewUser")
            {
                return false;
            }

            return true;
        }

    }
}