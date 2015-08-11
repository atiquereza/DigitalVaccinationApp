﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DigitalVaccination.Libs;
using DigitalVaccination.Models;

namespace DigitalVaccination.ApiController
{
    public class UserRolesController : System.Web.Http.ApiController
    {
        DBGateway aGateway = new DBGateway();
        // GET api/<controller>
        public List<UserRole> Get()
        {
            List<UserRole> userRoles = new List<UserRole>();

            HttpSessionStateBase Session = (HttpSessionStateBase)HttpContext.Current.Session["SessionBackup"];
            Dictionary<string, string> sessionDataDictionary = SessionHandler.GetSessionData(Session);
            int roleID = Convert.ToInt32(sessionDataDictionary["UserRoleId"]);
            string query = "select * from roles;";
            DataSet aDataSet = aGateway.Select(query);

            foreach (DataRow dataRow in aDataSet.Tables[0].Rows)
            {
                UserRole aUserRole = new UserRole();
                aUserRole.Id = Convert.ToInt32(dataRow["ID"].ToString());
                aUserRole.RoleName = dataRow["RoleName"].ToString();
                aUserRole.ParentRoleName = dataRow["ParentRoleName"].ToString();
                userRoles.Add(aUserRole);
            }

            string currentRole = userRoles.Find(i => i.Id == roleID).RoleName;

            List<UserRole> aList = new List<UserRole>();


           
            
            //List<string> checkedRoles = new List<string>();
            //foreach (UserRole aUserRole in userRoles)
            //{
            //    List<UserRole> recalledList = GetAsParentRole(userRoles, currentRole);
            //    aList.AddRange(recalledList);
            //    checkedRoles.Add(currentRole);

            //    foreach (UserRole ar in aList)
            //    {
            //        if (checkedRoles.Exists(i => i != ar.RoleName))
            //        {
            //            currentRole = ar.RoleName;
            //        }
            //    }

            //}

            //aList = aList.Distinct().ToList();
            //return aList;


            UserRoleManager roleManager = new UserRoleManager();
            List<UserRole> userRolesFinal = roleManager.GetRoleLevels(userRoles, roleID);


         

            return userRolesFinal;


        }


       

        private List<UserRole> GetAsParentRole(List<UserRole> userRole, string currentRole)
        {
            
           List<UserRole> listRoles = new List<UserRole>();

            foreach (UserRole aRole in userRole)
            {
                if (aRole.ParentRoleName == currentRole)
                {
                    listRoles.Add(aRole);
                }
            }

            return listRoles;
        }
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}