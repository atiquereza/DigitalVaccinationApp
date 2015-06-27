using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AngularJSDemo.Models;

namespace AngularJSDemo.ApiController
{
    public class CenterController : System.Web.Http.ApiController
    {
        DBGateway aGateway = new DBGateway();
        // GET api/<controller>
        public List<Centers> Get()
        {

            string query = "select centers.ID,centers.CentersName,centers.ThanasID,thanas.DistrictsID,thanas.ThanasName,districts.DistrictsName,centers.CentersAddress from centers,thanas,districts where districts.ID=thanas.DistrictsID and thanas.ID=centers.ThanasID;";

            DataSet aSet = aGateway.Select(query);

            // List<Vaccin> vaList = new List<Vaccin>();
            List<Centers> listCenters = new List<Centers>();
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
            return listCenters;
        }

        // GET api/<controller>/5
        public Centers Get(int ID)
        {
            string query = "select centers.ID,centers.CentersName,centers.ThanasID,thanas.DistrictsID,thanas.ThanasName,districts.DistrictsName,centers.CentersAddress from centers,thanas,districts where districts.ID=thanas.DistrictsID and thanas.ID=centers.ThanasID and centers.ID=" + ID + ";";

            DataSet aSet = aGateway.Select(query);

            // List<Vaccin> vaList = new List<Vaccin>();
            //  List<Centers> listCenters = new List<Centers>();
            Centers aCenters = new Centers();
            foreach (DataRow dataRow in aSet.Tables[0].Rows)
            {

                aCenters.ID = Convert.ToInt32(dataRow["ID"].ToString());
                aCenters.CentersName = (dataRow["CentersName"].ToString());
                aCenters.CentersAddress = (dataRow["CentersAddress"].ToString());
                aCenters.DistrictsName = (dataRow["DistrictsName"].ToString());
                aCenters.DistrictsID = Convert.ToInt32(dataRow["DistrictsID"].ToString());
                aCenters.ThanasID = Convert.ToInt32(dataRow["ThanasID"].ToString());
                aCenters.ThanasName = (dataRow["ThanasName"].ToString());

                //   listCenters.Add(aCenters);

            }
            return aCenters;
        }

        // POST api/<controller>
        public HttpResponseMessage Post(Centers aCenters)
        {
            if (ModelState.IsValid)
            {
                string query =
                    "INSERT INTO centers (CentersName, CentersAddress, ThanasID) VALUES (@name, @address, @thanaID);";

                Hashtable aHashtable = new Hashtable();
                aHashtable.Add("name", aCenters.CentersName);
                aHashtable.Add("address", aCenters.CentersAddress);
                aHashtable.Add("thanaID", aCenters.ThanasID);



                aGateway.Insert(query, aHashtable);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, aCenters);
                return response;
            }
            else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState); }

        }

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}


        public HttpResponseMessage Put(Centers aCenters)
        {
            if (ModelState.IsValid)
            {
                // string query ="INSERT INTO centers (CentersName, CentersAddress, ThanasID) VALUES (@name, @address, @thanaID);";
                string query = "UPDATE centers SET `ThanasID`=@thanaID, `CentersName`=@name, `CentersAddress`=@address WHERE  `ID`=" + aCenters.ID + ";";

                Hashtable aHashtable = new Hashtable();
                aHashtable.Add("name", aCenters.CentersName);
                aHashtable.Add("address", aCenters.CentersAddress);
                aHashtable.Add("thanaID", aCenters.ThanasID);



                aGateway.Update(query, aHashtable);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, aCenters);
                return response;
            }
            else { return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState); }

        }



        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            string query = string.Format("select * from centers where id=@id;");
            Hashtable aHashtable = new Hashtable();
            aHashtable.Add("id", id);
            DataSet aSet = aGateway.Select(query, aHashtable);
            if (aSet.Tables[0].Rows.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            // List<Vaccin> vaList = new List<Vaccin>();
            string delString = "DELETE FROM centers WHERE  id=@id;";
            aGateway.Delete(delString, aHashtable);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }
    }
}