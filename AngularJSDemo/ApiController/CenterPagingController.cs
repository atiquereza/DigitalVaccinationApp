using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularJSDemo.Controllers;
using AngularJSDemo.Libs;
using AngularJSDemo.Models;

namespace AngularJSDemo.ApiController
{
    public class CenterPagingController : System.Web.Http.ApiController
    {
        private readonly DBGateway aGateway;

        public CenterPagingController()
        {
            aGateway = new DBGateway();
        }
        // GET api/<controller>
        public PagedResult<Centers> Get(int pageNo = 1, int pageSize = 50, [FromUri] string[] sort = null, string search = null)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            // search = textInfo.ToTitleCase(search);
            // Determine the number of records to skip
            int skip = (pageNo - 1) * pageSize;
            string query = "select centers.ID,centers.CentersName,centers.ThanasID,thanas.DistrictsID,thanas.ThanasName,districts.DistrictsName,centers.CentersAddress from centers,thanas,districts where districts.ID=thanas.DistrictsID and thanas.ID=centers.ThanasID;";
            DataSet aSet = aGateway.Select(query);
            //IQueryable<Customer> queryable;
            List<UserInfo> aUsersList = new List<UserInfo>();
            List<Centers> listCenters = new List<Centers>();
            //UserInfo aUserInfo = new UserInfo();
            foreach (DataRow dataRow in aSet.Tables[0].Rows)
            {

                Centers aCenters = new Centers();
                aCenters.ID = Convert.ToInt32(dataRow["ID"].ToString());
                aCenters.CentersName = (dataRow["CentersName"].ToString());
                aCenters.CentersAddress = (dataRow["CentersAddress"].ToString());
                aCenters.DistrictsName = (dataRow["DistrictsName"].ToString());
                aCenters.DistrictsID = Convert.ToInt32(dataRow["DistrictsID"].ToString());
                aCenters.ThanasID = Convert.ToInt32(dataRow["ThanasID"].ToString());
                aCenters.ThanasName = (dataRow["ThanasName"].ToString());

                listCenters.Add(aCenters);

            }
            IQueryable<Centers> queryable = listCenters.AsQueryable();

            // Apply the search
            if (!String.IsNullOrEmpty(search))
            {
                string[] searchElements = search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string searchElement in searchElements)
                {
                    string element = searchElement;
                    queryable = queryable.Where(c => c.CentersName.ToLower().Contains(element.ToLower()) || c.ThanasName.ToLower().Contains(element.ToLower()) || c.DistrictsName.ToLower().Contains(element.ToLower()) || c.CentersAddress.ToLower().Contains(element.ToLower()) );
                }
                pageNo = 1;
            }

            // Add the sorting
            if (sort != null)
            {
                queryable = queryable.ApplySorting(sort);
                //pageNo = 1;
            }
            else
                queryable = queryable.OrderBy(c => c.ID);
            List<Centers> aList = new List<Centers>();
            // Get the total number of records
            int totalItemCount = queryable.Count();
            var centers = aList;
            // Retrieve the customers for the specified page
            if (String.IsNullOrEmpty(search))
            {
                centers = queryable
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                centers = queryable
                    //.Skip(0)
                    //.Take(pageSize)
                .ToList();
                if (centers.Count >= skip)
                {
                    centers = centers.Skip(skip).Take(pageSize).ToList();
                }
                else { centers = centers.Skip(0).Take(pageSize).ToList(); }

            }

            // Return the paged results
            return new PagedResult<Centers>(centers, pageNo, pageSize, totalItemCount);
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