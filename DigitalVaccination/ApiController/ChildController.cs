using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DigitalVaccination.Libs;
using DigitalVaccination.Models;

namespace DigitalVaccination.ApiController
{
    public class ChildController : System.Web.Http.ApiController
    {
        // GET api/<controller>
        public IEnumerable<ChildInfo> Get()
        {
            HttpSessionStateBase Session = (HttpSessionStateBase)HttpContext.Current.Session["SessionBackup"];
            Dictionary<string, string> session = SessionHandler.GetSessionData(Session);
            string query = "select * from childinfo where parentid=@ParentId;";
            Hashtable aHashtable = new Hashtable();
            aHashtable.Add("ParentId",session["UserId"]);

            DBGateway aGateway= new DBGateway();
            DataSet aSet = aGateway.Select(query,aHashtable);

            List<ChildInfo> aList = new List<ChildInfo>();
            int index = 1;
            foreach (DataRow aRow in aSet.Tables[0].Rows)
            {
                ChildInfo aInfo = new ChildInfo();
                aInfo.ID = index;
                aInfo.Name = aRow["Name"].ToString();
                aInfo.ParentID = Convert.ToInt32(aRow["ParentID"].ToString());
                aInfo.BirthCertificateID = aRow["BirthCertificateID"].ToString();
                aInfo.MotherName = aRow["MotherName"].ToString();
                aInfo.BirthDate = Convert.ToDateTime(aRow["Birthdate"].ToString());
                aList.Add(aInfo);
                index++;
            }


            return aList;
        }

        //// GET api/<controller>/5
        //public ChildInfo Get(int id)
        //{
            
        //}

        // POST api/<controller>
        public HttpResponseMessage Post(ChildInfo childInfo)
        {

            if (ModelState.IsValid)
            {
                HttpSessionStateBase Session = (HttpSessionStateBase)HttpContext.Current.Session["SessionBackup"];
                Dictionary<string, string> session = SessionHandler.GetSessionData(Session);
                string query =
                    "INSERT INTO `tikaappdb`.`childinfo` (`ParentID`, `Name`, `Birthdate`,`BirthCertificateID`,`MotherName`) " +
                    "VALUES (@ParentId, @Name, @Date,@BirthCertificateID,@MotherName);";
                Hashtable aHashtable = new Hashtable();
                aHashtable.Add("ParentId", session["UserId"]);
                aHashtable.Add("Name", childInfo.Name);
                aHashtable.Add("Date", childInfo.BirthDate);
                aHashtable.Add("BirthCertificateID", childInfo.BirthCertificateID);
                aHashtable.Add("MotherName", childInfo.MotherName);
                DBGateway aGateway = new DBGateway();
                aGateway.Insert(query, aHashtable);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, childInfo);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
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