using System;
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

            UserRole superUserRole= userRoles.Where(c => c.ParentRoleName == c.RoleName).First();
           //  = userRoles.Take(userRoles.Count).
            List<string> rolsList = (from o in userRoles select o.RoleName).Distinct().ToList();
            List<string> parentList = (from o in userRoles select o.ParentRoleName).Distinct().ToList();





            return userRoles;
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