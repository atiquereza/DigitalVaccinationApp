using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DigitalVaccination.Libs
{
    public class UserRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string ParentRoleName { get; set; }
        public int Level { get; set; }
    }
    public class UserRoleManager
    {
        public List<UserRole> GetRoleLevels(int roleID)
        {
            DBGateway aGateway=new DBGateway();
            string query = "select * from roles;";
            DataSet aDataSet = aGateway.Select(query);
            List<UserRole> userRoles = new List<UserRole>();
            foreach (DataRow dataRow in aDataSet.Tables[0].Rows)
            {
                UserRole aUserRole = new UserRole();
                aUserRole.Id = Convert.ToInt32(dataRow["ID"].ToString());
                aUserRole.RoleName = dataRow["RoleName"].ToString();
                aUserRole.ParentRoleName = dataRow["ParentRoleName"].ToString();
                userRoles.Add(aUserRole);
            }



            List<UserRole> userRolesFinal =
                userRoles.Where(c => c.ParentRoleName == c.RoleName).ToList().Select(c =>
                {
                    c.Level = 0;
                    return c;
                }
                ).ToList();

            bool continueParse = true;
            List<UserRole> tempList = userRolesFinal;
            int j = 1;
            while (continueParse)
            {
                List<UserRole> childListLevel = new List<UserRole>();
                foreach (UserRole aRole in tempList)
                {
                    List<UserRole> childList = userRoles.Where(c => c.ParentRoleName == aRole.RoleName && !userRolesFinal.Any(p2 => p2.Id == c.Id)).ToList();
                    childList = childList.Select(c =>
                    {
                        c.Level = j;
                        return c;
                    }).ToList();
                    if (childList.Count > 0)
                    {
                        childListLevel.AddRange(childList);
                    }
                }
                if (childListLevel.Count > 0)
                {
                    // userRolesFinal.AddRange(childListLevel);
                    userRolesFinal = userRolesFinal.Concat(childListLevel).ToList();
                    tempList.Clear();
                    tempList.AddRange(childListLevel);
                    childListLevel.Clear();
                    j++;
                }
                else
                {
                    continueParse = false;
                }



            }


            int userLevel = userRolesFinal.Where(c => c.Id == roleID).First().Level;

            List<UserRole> removeRoles = userRolesFinal.Where(s => s.Level <= userLevel).ToList();

            foreach (UserRole aRemovableRole in removeRoles)
            {
                if (aRemovableRole.Id != roleID)
                {
                    userRolesFinal.Remove(aRemovableRole);
                }
            }
           

            return userRolesFinal;
        }


        public int GetRoleID()
        {
            HttpSessionStateBase Session = (HttpSessionStateBase)HttpContext.Current.Session["SessionBackup"];
            Dictionary<string, string> sessionDataDictionary = SessionHandler.GetSessionData(Session);
            int roleID = Convert.ToInt32(sessionDataDictionary["UserRoleId"]);
            return roleID;
        }
    }
}