using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DigitalVaccination.Controllers;

using DigitalVaccination.Libs;

namespace ManiacProject.Views.Classes
{
    public class MenuItem
    {
        public string Id { set; get; }
        public string MenuName { set; get; }
        public string MenuParentName { set; get; }
        public string Controller { set; get; }
        public string Action { set; get; }

    }

    public static class MenuItems
    {
        public static string GetMenuItems(HttpSessionStateBase Session)
        {
            
            string menuString = string.Empty;
            if (Session.Count > 0)
            {
                List<MenuItem> menu = GetMenuItemLists(Session);
                UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);


                menuString += "<ul id=\"menuitems\">\n";
                menuString += "<ul id=\"menucontainer\">\n";
                List<MenuItem> mainMenu = menu.Where(b => b.MenuName == b.MenuParentName).ToList();

                foreach (MenuItem menuItem in mainMenu)
                {
                    List<MenuItem> subMenu = menu.Where(b=>b.MenuParentName == menuItem.MenuParentName && b.MenuName != b.MenuParentName).ToList();
                    menuString += "<li><a href=\"";
                    menuString += url.Action(menuItem.Action, menuItem.Controller);
                    menuString += "\">" + menuItem.MenuName + "</a>\n";
                    if(subMenu.Count > 0)
                    {
                        menuString += "<ul>\n";
                        foreach (MenuItem item in subMenu)
                        {
                            List<MenuItem> childMenu = menu.Where(b => b.MenuParentName == item.MenuName).ToList();
                            menuString += "<li style=\"width:150px;\"><a href=\"" + url.Action(item.Action, item.Controller) + "\">" + item.MenuName + "</a>\n";

                            if (childMenu.Count > 0)
                            {
                                menuString += " <ul>\n";
                                foreach (MenuItem aChildMenu in childMenu)
                                {
                                    menuString += "<li style=\"width:150px;\"><a href=\"" + url.Action(aChildMenu.Action, aChildMenu.Controller) + "\">" + aChildMenu.MenuName + "</a></li>\n";
                                }
                                menuString += "</ul>\n";
                            }
                            menuString += "</li>\n";
                        }
                        menuString += "</ul>\n";
                    }
                    menuString += "</li>\n";
                }
                menuString += "<li><a href=\"";
                menuString += url.Action("LogOut", "Account");
                menuString += "\">Log Out</a></li>\n";
                menuString += "</ul>\n";
                menuString += "</ul>\n";

            }
            return menuString;

        }



        public static List<MenuItem> GetMenuItemLists(HttpSessionStateBase Session)
        {

            Dictionary<string, string> sessionData = SessionHandler.GetSessionData(Session);

            List<MenuItem> menu = new List<MenuItem>();
            
            DBGateway aGateway = new DBGateway();
            DataSet aDataSet = aGateway.Select("select * from appmenuitems, appviews where appmenuitems.AppViewId = appviews.Id and " + sessionData["RoleName"] + " = 1 order by menuorder asc, submenuorder asc");
            
            foreach (DataRow dataRow in aDataSet.Tables[0].Rows)
            {
                MenuItem aMenuItem = new MenuItem();
                aMenuItem.Id = dataRow["Id"].ToString();
                aMenuItem.MenuName = dataRow["MenuName"].ToString();
                aMenuItem.MenuParentName = dataRow["MenuParentName"].ToString();
                aMenuItem.Controller = dataRow["Controller"].ToString();
                aMenuItem.Action = dataRow["Action"].ToString();
                menu.Add(aMenuItem);
            }
            return menu;
        }
    }
}
