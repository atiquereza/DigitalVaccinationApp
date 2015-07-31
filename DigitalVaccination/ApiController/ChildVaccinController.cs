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
    public class ChildVaccinController : System.Web.Http.ApiController
    {
        // GET api/<controller>
        public IEnumerable<ChildVaccinInfo> Get(int id)
        {
            HttpSessionStateBase Session = (HttpSessionStateBase)HttpContext.Current.Session["SessionBackup"];
            Dictionary<string, string> session = SessionHandler.GetSessionData(Session);
            string query = "select * from childinfo where parentid=@ParentId and ID=@Id;";
            Hashtable aHashtable = new Hashtable();
            aHashtable.Add("ParentId", session["UserId"]);
            aHashtable.Add("Id", id);

            DBGateway aGateway = new DBGateway();
            DataSet aSet = aGateway.Select(query, aHashtable);

            ChildInfo childInfo = new ChildInfo();
            foreach (DataRow aRow in aSet.Tables[0].Rows)
            {
                childInfo.ID = Convert.ToInt32(aRow["ID"].ToString());
                childInfo.Name = aRow["Name"].ToString();
                childInfo.ParentID = Convert.ToInt32(aRow["ParentID"].ToString());
                childInfo.BirthCertificateID = aRow["BirthCertificateID"].ToString();
                childInfo.MotherName = aRow["MotherName"].ToString();
                childInfo.BirthDate = Convert.ToDateTime(aRow["Birthdate"].ToString());
                
            }



            query = "Select * from ChildVaccin where ChildID=@ChildID;";
            aHashtable = new Hashtable() { { "ChildID", id } };
            aSet = aGateway.Select(query,aHashtable);

            Dictionary<string, Dictionary<string, string>> childGivenVaccin = new Dictionary<string, Dictionary<string, string>>();

            foreach (DataRow aRow in aSet.Tables[0].Rows)
            {
                Dictionary<string, string> aDictionary = new Dictionary<string, string>();
                aDictionary.Add("VaccinID", aRow["VaccinID"].ToString());
                string index = "ChildID-" + aRow["ChildID"].ToString() + "_VaccinID-" + aRow["VaccinID"].ToString();
                childGivenVaccin.Add(index, aDictionary);
            }





            query = "select * from vaccineinfo;";            
            aSet = aGateway.Select(query);


            
            List<ChildVaccinInfo> aList = new List<ChildVaccinInfo>();

            foreach (DataRow aRow in aSet.Tables[0].Rows)
            {
                string index = "ChildID-" + id.ToString() + "_VaccinID-" + aRow["ID"].ToString();

                ChildVaccinInfo aInfo = new ChildVaccinInfo();
                aInfo.ChildId = id;
                aInfo.ID = Convert.ToInt32(aRow["ID"].ToString());
                aInfo.VaccinName = aRow["VaccineName"].ToString();
                aInfo.LastNotificationSent = DateTime.Now;
                aInfo.Date = Convert.ToDateTime(childInfo.BirthDate.AddDays(Convert.ToInt32(aRow["StartDay"].ToString())));
                aInfo.DueDate = Convert.ToDateTime(childInfo.BirthDate.AddDays(Convert.ToInt32(aRow["EndDay"].ToString())));
                
                if(childGivenVaccin.ContainsKey(index))
                {
                    aInfo.Status = "Yes";
                }
                else
                {
                    aInfo.Status = "No";
                }
                aInfo.Amount = Convert.ToInt32(aRow["Amount"].ToString());
                aInfo.Type = aRow["Type"].ToString();
                aList.Add(aInfo);
            }


            

            return aList;
        }


        public HttpResponseMessage Post(TikaStatusChangeInfo childInfo)
        {
            DBGateway aGateway = new DBGateway();
            if (ModelState.IsValid)
            {
                string query = "select * from childvaccin where ChildID=@ChildID and VaccinID=@VaccinID;";
                Hashtable aHashtable = new Hashtable() { { "ChildID", childInfo.ChildId }, { "VaccinID", childInfo.TikaId } };
                DataSet aSet = aGateway.Select(query,aHashtable);

                if(aSet.Tables[0].Rows.Count > 0)
                {
                    query = "delete from childvaccin where ChildID=@ChildID and VaccinID=@VaccinID;";
                    aGateway.Delete(query, aHashtable);
                }else
                {
                    query = "INSERT INTO `tikaappdb`.`childvaccin` (`ChildID`, `VaccinID`, `Status`) VALUES (@ChildID, @VaccinID, @Status)";
                    aHashtable = new Hashtable() { { "ChildID", childInfo.ChildId }, { "VaccinID", childInfo.TikaId },{"@Status", 1} };
                    aGateway.Insert(query, aHashtable);
                }
                
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, childInfo);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }

    
}