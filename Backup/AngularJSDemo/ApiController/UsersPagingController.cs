using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq.Dynamic;
using DigitalVaccination.Controllers;
using DigitalVaccination.Libs;
using DigitalVaccination.Models;

namespace DigitalVaccination.ApiController
{
    public class UsersPagingController : System.Web.Http.ApiController
    {
        //private readonly DemoContext demoContext;
        private readonly DBGateway aGateway;

        public UsersPagingController()
        {
            // demoContext = new DemoContext();
            aGateway = new DBGateway();
        }

        // GET api/customers
        public PagedResult<UserInfo> Get(int pageNo = 1, int pageSize = 50, [FromUri] string[] sort = null, string search = null)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            // search = textInfo.ToTitleCase(search);
            // Determine the number of records to skip
            int skip = (pageNo - 1) * pageSize;
            string query = "SELECT * FROM `userInfo` LIMIT 1000;";
            DataSet aSet = aGateway.Select(query);
            //IQueryable<Customer> queryable;
            List<UserInfo> aUsersList = new List<UserInfo>();
            //UserInfo aUserInfo = new UserInfo();
            foreach (DataRow dataRow in aSet.Tables[0].Rows)
            {
               
                UserInfo aUserInfo = new UserInfo();
                aUserInfo.Id = Convert.ToInt32(dataRow["ID"].ToString());
                aUserInfo.UserId = Convert.ToInt32(dataRow["UserID"].ToString());
                aUserInfo.UserName = dataRow["UserName"].ToString();
                aUserInfo.FullName = dataRow["FullName"].ToString();
                aUserInfo.FatherName = dataRow["FatherName"].ToString();
                aUserInfo.MotherName = dataRow["MotherName"].ToString();
                aUserInfo.PhoneNumber = dataRow["CellNumber"].ToString();
                aUserInfo.BirthDate = (DateTime)dataRow["BirthDay"];
                aUserInfo.CurrentAddress = dataRow["CurrentAddress"].ToString();
                aUserInfo.PermanentAddress = dataRow["PermanentAddress"].ToString();
                aUserInfo.BirthCertificateID = dataRow["BirthCertificateID"].ToString();


                aUsersList.Add(aUserInfo);

            }
            IQueryable<UserInfo> queryable = aUsersList.AsQueryable();

            // Apply the search
            if (!String.IsNullOrEmpty(search))
            {
                string[] searchElements = search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string searchElement in searchElements)
                {
                    string element = searchElement;
                    queryable = queryable.Where(c => c.UserName.ToLower().Contains(element.ToLower()) || c.FullName.ToLower().Contains(element.ToLower()) || c.FatherName.ToLower().Contains(element.ToLower()) || c.MotherName.ToLower().Contains(element.ToLower()) || c.PhoneNumber.ToLower().Contains(element.ToLower()));
                }
                pageNo = 1;
            }

            // Add the sorting
            if (sort != null)
            {
                queryable = queryable.ApplySorting(sort);
                pageNo = 1;
            }
            else
                queryable = queryable.OrderBy(c => c.Id);
            List<UserInfo> aList = new List<UserInfo>();
            // Get the total number of records
            int totalItemCount = queryable.Count();
            var users = aList;
            // Retrieve the customers for the specified page
            if (String.IsNullOrEmpty(search))
            {
                users = queryable
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                users = queryable
                    //.Skip(0)
                    //.Take(pageSize)
                .ToList();
                if (users.Count >= skip)
                {
                    users = users.Skip(skip).Take(pageSize).ToList();
                }
                else { users = users.Skip(0).Take(pageSize).ToList(); }

            }

            // Return the paged results
            return new PagedResult<UserInfo>(users, pageNo, pageSize, totalItemCount);
        }
    }
}