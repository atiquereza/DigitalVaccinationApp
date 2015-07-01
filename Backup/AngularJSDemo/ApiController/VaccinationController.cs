using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using DigitalVaccination.Libs;
using DigitalVaccination.Models;

namespace DigitalVaccination.ApiController
{
    public class VaccinationController : System.Web.Http.ApiController
    {
        

        DBGateway aGateway = new DBGateway();
        // GET api/<controller>
        public IEnumerable<Vaccin> Get()
        {
           // return new string[] { "value1", "value2" };

            string query = "select * from vaccineinfo";
            DataSet aSet = aGateway.Select(query);

            List<Vaccin> vaList = new List<Vaccin>();

            foreach (DataRow dataRow in aSet.Tables[0].Rows)
            {
                Vaccin aVaccin = new Vaccin();
                aVaccin.ID = Convert.ToInt32(dataRow["ID"].ToString());
                aVaccin.Name = dataRow["VaccineName"].ToString();
                aVaccin.StartDay = Convert.ToInt32(dataRow["StartDay"].ToString());
                aVaccin.EndDay = Convert.ToInt32(dataRow["EndDay"].ToString());
                aVaccin.Description = dataRow["Description"].ToString();
                vaList.Add(aVaccin);
            }

            return vaList;
        }

        // GET api/<controller>/5
        public Vaccin Get(int id)
        {
            string query = "select * from vaccineinfo where ID=@id";
            Hashtable aHashtable = new Hashtable();
            aHashtable.Add("id", id);
            DataSet aSet = aGateway.Select(query,aHashtable);

           // List<Vaccin> vaList = new List<Vaccin>();
            Vaccin aVaccin = new Vaccin();
            foreach (DataRow dataRow in aSet.Tables[0].Rows)
            {

                aVaccin.ID = Convert.ToInt32(dataRow["ID"].ToString());
                aVaccin.Name = dataRow["VaccineName"].ToString();
                aVaccin.StartDay = Convert.ToInt32(dataRow["StartDay"].ToString());
                aVaccin.EndDay = Convert.ToInt32(dataRow["EndDay"].ToString());
                aVaccin.Description = dataRow["Description"].ToString();

                
            }

            return aVaccin;
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}


        // POST api/employee
        public HttpResponseMessage Post(Vaccin aVaccin)
        {
            if (ModelState.IsValid)
            {
                //db.Employees.Add(model);
                //db.SaveChanges();

               
                string query = "INSERT INTO vaccineinfo (VaccineName, StartDay, EndDay,Description) VALUES (@name, @start_day, @end_day,@description);";
               
                Hashtable aHashtable = new Hashtable();
                aHashtable.Add("name", aVaccin.Name);
                aHashtable.Add("start_day", aVaccin.StartDay);
                aHashtable.Add("end_day", aVaccin.EndDay);
                aHashtable.Add("description", aVaccin.Description);
               

                aGateway.Insert(query, aHashtable);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, aVaccin);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }



        // PUT api/Vaccination/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        public HttpResponseMessage Put(Vaccin aVaccin)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();

                //UPDATE `tikaappdb`.`vaccination` SET `start_day`=30 WHERE  `id`=2;
                string query1 = "UPDATE vaccineinfo SET VaccineName=@name, StartDay=@start_day, EndDay=@end_time,Description=@description WHERE  ID=@id;";
              
                Hashtable aHashtable = new Hashtable();
                aHashtable.Add("name",aVaccin.Name);
                aHashtable.Add("start_day", aVaccin.StartDay);
                aHashtable.Add("end_time", aVaccin.EndDay);
                aHashtable.Add("id", aVaccin.ID);
                aHashtable.Add("description", aVaccin.Description);
                aGateway.Update(query1,aHashtable);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, aVaccin);
                return response;
               
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        // DELETE api/Vaccination/5
        public HttpResponseMessage Delete(int id)
        {
            string query = string.Format("select * from vaccineinfo where id=@id;");
            Hashtable aHashtable=new Hashtable();
            aHashtable.Add("id",id);
            DataSet aSet = aGateway.Select(query,aHashtable);
            if (aSet.Tables[0].Rows.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            // List<Vaccin> vaList = new List<Vaccin>();
            string delString = "DELETE FROM vaccineinfo WHERE  id=@id;";
            aGateway.Delete(delString, aHashtable);
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }
    }
}