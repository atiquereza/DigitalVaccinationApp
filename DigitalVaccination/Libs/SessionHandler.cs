using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;


namespace DigitalVaccination.Libs
{
    public static class SessionHandler
    {
        
        public static Dictionary<string, string> GetSessionData(HttpSessionStateBase Session)
        {
            Dictionary<string, string> sessionData = new Dictionary<string, string>();
            if (Session != null)
            {
                sessionData["LogInValue"] = EncrDecrAction.Decrypt(Session["LogInValue"].ToString(), true);
                sessionData["LoggedIn"] = EncrDecrAction.Decrypt(Session["LoggedIn"].ToString(), true);
                sessionData["UserId"] = EncrDecrAction.Decrypt(Session["UserId"].ToString(), true);
                sessionData["UserRoleId"] = EncrDecrAction.Decrypt(Session["UserRoleId"].ToString(), true);
                sessionData["RoleName"] = EncrDecrAction.Decrypt(Session["RoleName"].ToString(), true);
                sessionData["UserName"] = EncrDecrAction.Decrypt(Session["UserName"].ToString(), true);
                sessionData["ParentRoleName"] = EncrDecrAction.Decrypt(Session["ParentRoleName"].ToString(), true);

            }
            return sessionData;
        }

        
        public static HttpSessionStateBase SetSessionData(Dictionary<string,string> UserData, HttpSessionStateBase Session)
        {
            Dictionary<string, string> sessionData = new Dictionary<string, string>();
            if(Session.Count > 0)
            {

                if (Session["LogInValue"] != null)
                    sessionData["LogInValue"] = EncrDecrAction.Decrypt(Session["LogInValue"].ToString(), true);
                if (Session["LoggedIn"] != null)   
                    sessionData["LoggedIn"] = EncrDecrAction.Decrypt(Session["LoggedIn"].ToString(), true);
                if (Session["LoggedIn"] != null)
                    sessionData["UserId"] = EncrDecrAction.Decrypt(Session["UserId"].ToString(), true);
                if (Session["UserRoleId"] != null)
                    sessionData["UserRoleId"] = EncrDecrAction.Decrypt(Session["RoleId"].ToString(), true);
                if (Session["UserName"] != null)
                    sessionData["UserName"] = EncrDecrAction.Decrypt(Session["UserName"].ToString(), true);
                if (Session["RoleName"] != null)
                    sessionData["RoleName"] = EncrDecrAction.Decrypt(Session["RoleName"].ToString(), true);
                if (Session["ParentRoleName"] != null)
                    sessionData["ParentRoleName"] = EncrDecrAction.Decrypt(Session["ParentRoleName"].ToString(), true);
            }

            else
            {
                sessionData["LogInValue"] = EncrDecrAction.Decrypt(EncrDecrAction.Encrypt(UserData["LogInValue"], true), true);
                sessionData["LoggedIn"] = EncrDecrAction.Decrypt(EncrDecrAction.Encrypt(UserData["UserName"], true), true);
                sessionData["UserId"] = EncrDecrAction.Decrypt(EncrDecrAction.Encrypt(UserData["ID"], true), true);
                sessionData["UserRoleId"] = EncrDecrAction.Decrypt(EncrDecrAction.Encrypt(UserData["UserRoleId"], true), true);
                sessionData["UserName"] = EncrDecrAction.Decrypt(EncrDecrAction.Encrypt(UserData["UserName"], true), true);
                sessionData["RoleName"] = EncrDecrAction.Decrypt(EncrDecrAction.Encrypt(UserData["RoleName"], true), true);
                sessionData["ParentRoleName"] = EncrDecrAction.Decrypt(EncrDecrAction.Encrypt(UserData["ParentRoleName"], true), true); 
            }
            
            Session.Clear();

            Session["LogInValue"] = EncrDecrAction.Encrypt(sessionData["LogInValue"], true);
            Session["LoggedIn"] = EncrDecrAction.Encrypt(sessionData["LoggedIn"], true);
            Session["UserId"] = EncrDecrAction.Encrypt(sessionData["UserId"], true);
            Session["UserRoleId"] = EncrDecrAction.Encrypt(sessionData["UserRoleId"], true);
            Session["UserName"] = EncrDecrAction.Encrypt(sessionData["UserName"], true);
            Session["RoleName"] = EncrDecrAction.Encrypt(sessionData["RoleName"], true);
            Session["ParentRoleName"] = EncrDecrAction.Encrypt(sessionData["ParentRoleName"], true);


            string encr = EncrDecrAction.Encrypt(
                            EncrDecrAction.Encrypt(EncrDecrAction.Encrypt(Session["UserId"].ToString(), true), true)
                          + EncrDecrAction.Encrypt(EncrDecrAction.Encrypt(Session["UserRoleId"].ToString(), true), true)
                          + EncrDecrAction.Encrypt(EncrDecrAction.Encrypt(Session["UserName"].ToString(), true), true)
                          + EncrDecrAction.Encrypt(EncrDecrAction.Encrypt(Session["RoleName"].ToString(), true), true)
                          + EncrDecrAction.Encrypt(EncrDecrAction.Encrypt(Session["ParentRoleName"].ToString(), true), true), true);
            Session["SRES"] = encr;
           
            HttpContext.Current.Session["SessionBackup"] = Session;
            return Session;
        }

        
    }
}