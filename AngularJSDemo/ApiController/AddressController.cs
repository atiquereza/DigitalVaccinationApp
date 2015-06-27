using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularJSDemo.Models;

namespace AngularJSDemo.ApiController
{
    public class AddressController : System.Web.Http.ApiController
    {
        DBGateway aGateway = new DBGateway();
        // GET api/Address
        public List<Districts> Get()
        {
            string query = "select * from districts";

            DataSet aSet = aGateway.Select(query);

            // List<Vaccin> vaList = new List<Vaccin>();
            List<Districts> listDistricts = new List<Districts>();
            foreach (DataRow dataRow in aSet.Tables[0].Rows)
            {
                Districts aDistricts = new Districts();
                aDistricts.ID = Convert.ToInt32(dataRow["ID"].ToString());
                aDistricts.DistrictsName = (dataRow["DistrictsName"].ToString());

                listDistricts.Add(aDistricts);

            }
            return listDistricts;
        }




        // GET api/Address/5
        public List<Thanas> Get(int id)
        {
            string query = "select * from Thanas where DistrictsID=" + id;

            DataSet aSet = aGateway.Select(query);


            List<Thanas> listThanas = new List<Thanas>();
            foreach (DataRow dataRow in aSet.Tables[0].Rows)
            {
                Thanas aThanas = new Thanas();
                aThanas.ID = Convert.ToInt32(dataRow["ID"].ToString());
                aThanas.DistrictsID = Convert.ToInt32(dataRow["DistrictsID"].ToString());
                aThanas.ThanasName = (dataRow["ThanasName"].ToString());

                listThanas.Add(aThanas);

            }
            return listThanas;
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