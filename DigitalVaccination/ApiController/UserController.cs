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
    public class UserController : System.Web.Http.ApiController
    {
        DBGateway aGateway = new DBGateway();
        // GET api/<controller>
        //[ApiAuthorize]
        public UserInfo Get()
        {


            string query = "select * from userinfo";
            DataSet aSet = aGateway.Select(query);

            List<UserInfo> userInfos = new List<UserInfo>();
            UserInfo aUserInfo = new UserInfo();
            foreach (DataRow dataRow in aSet.Tables[0].Rows)
            {

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

            }

            query = "select * from childInfo where ParentID=1";
            DataSet anotherDataSet = aGateway.Select(query);
            List<ChildInfo> childList = new List<ChildInfo>();
            foreach (DataRow dataRow in anotherDataSet.Tables[0].Rows)
            {
                ChildInfo aChildInfo = new ChildInfo();
                aChildInfo.ID = Convert.ToInt32(dataRow["ID"].ToString());
                aChildInfo.ParentID = Convert.ToInt32(dataRow["ParentID"].ToString());
                aChildInfo.Name = dataRow["Name"].ToString();
                aChildInfo.BirthCertificateID = dataRow["BirthCertificateID"].ToString();
                aChildInfo.BirthDate = (DateTime)dataRow["Birthdate"];
                childList.Add(aChildInfo);
            }
            aUserInfo.Childs = childList;
            return aUserInfo;
        }

        [ApiAuthorize]
        public UserInfo Get(int id)
        {
            string query = "select * from userinfo where id=@id;";
            string query1 = "select userinfo.ID,userinfo.UserID,userinfo.UserName,userinfo.FullName,userinfo.FatherName,userinfo.MotherName,userinfo.CellNumber,userinfo.BirthDay,userinfo.CurrentAddress,userinfo.PermanentAddress,userinfo.BirthCertificateID,users.UserRoleId from userinfo,users where userinfo.id=@id and users.ID=userInfo.UserId;";
            Hashtable aTable = new Hashtable() { { "id", id } };
            DataSet aSet = aGateway.Select(query1, aTable);

            List<UserInfo> userInfos = new List<UserInfo>();
            UserInfo aUserInfo = new UserInfo();
            foreach (DataRow dataRow in aSet.Tables[0].Rows)
            {

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
                aUserInfo.UserRole = Convert.ToInt32(dataRow["UserRoleId"].ToString());

            }

            query = "select * from childInfo where ParentID=1";
            DataSet anotherDataSet = aGateway.Select(query);
            List<ChildInfo> childList = new List<ChildInfo>();
            foreach (DataRow dataRow in anotherDataSet.Tables[0].Rows)
            {
                ChildInfo aChildInfo = new ChildInfo();
                aChildInfo.ID = Convert.ToInt32(dataRow["ID"].ToString());
                aChildInfo.ParentID = Convert.ToInt32(dataRow["ParentID"].ToString());
                aChildInfo.Name = dataRow["Name"].ToString();
                aChildInfo.BirthCertificateID = dataRow["BirthCertificateID"].ToString();
                aChildInfo.BirthDate = (DateTime)dataRow["Birthdate"];
                childList.Add(aChildInfo);
            }
            aUserInfo.Childs = childList;
            return aUserInfo;
        }

        // POST api/<controller>
        public HttpResponseMessage Put(UserInfo aUser)
        {
            if (ModelState.IsValid)
            {
                string query = "UPDATE `tikaappdb`.`userinfo` SET `UserName`=@userName, `FullName`=@fullName, `FatherName`=@fathersName, `MotherName`=@mothersName, `CellNumber`=@phoneNumber, `BirthDay`=@birthdate, `CurrentAddress`=@currentAddress, `PermanentAddress`=@permanentAddress, `BirthCertificateID`=@birthCertificate WHERE  `ID`=@id;";
                Hashtable aHashtable = new Hashtable();
                aHashtable.Add("fullName", aUser.FullName);
                aHashtable.Add("id", aUser.Id);
                aHashtable.Add("userName", aUser.UserName);
                aHashtable.Add("fathersName", aUser.FatherName);
                aHashtable.Add("mothersName", aUser.MotherName);
                aHashtable.Add("phoneNumber", aUser.PhoneNumber);
                aHashtable.Add("birthdate", aUser.BirthDate);
                aHashtable.Add("birthCertificate", aUser.BirthCertificateID);
                aHashtable.Add("currentAddress", aUser.CurrentAddress);
                aHashtable.Add("permanentAddress", aUser.PermanentAddress);
                aGateway.Update(query, aHashtable);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, aUser);
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        // PUT api/<controller>/5
        public HttpResponseMessage Post(UserInfo aUser)
        {
            if (ModelState.IsValid)
            {

                string phoneQuery = "Select * from userinfo where  CellNumber=" + aUser.PhoneNumber + " ;";
                string userNameQuery = "Select * from users where  UserName='" + aUser.UserName + "' ;";

                DataSet aDataSet = aGateway.Select(phoneQuery);
                DataSet anotherDataSet = aGateway.Select(userNameQuery);
                if (aDataSet.Tables[0].Rows.Count > 0)
                {
                    var message = string.Format("Duplicate Phone Number");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, err);
                }
                else if (anotherDataSet.Tables[0].Rows.Count > 0)
                {

                    var message = string.Format("Duplicate UserName");
                    HttpError err = new HttpError(message);
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, err);
                }
                else
                {

                    string usersQuery = "insert into users(UserName,UserCellNumber,UserRoleId) values (@UserName,@CellNumber,"+aUser.UserRole+") ";
                    string userInfoQuery =
                        "INSERT INTO `tikaappdb`.`userinfo` (`UserId`, `UserName`, `FullName`, `FatherName`, `MotherName`, `CellNumber`, `BirthDay`, `CurrentAddress`, `PermanentAddress`, `BirthCertificateID`) VALUES ((select ID from users where UserName='" + aUser.UserName + "'), @UserName, @FullName, @FatherName, @MotherName, @CellNumber, @BirthDay, @CurrentAddress, @PermanentAddress, @BirthCertificateID);";
                    Hashtable aHashtable = new Hashtable();
                    aHashtable.Add("id", aUser.Id);
                  // aHashtable.Add("UserID", 101);
                    aHashtable.Add("UserName", aUser.UserName);
                    aHashtable.Add("FullName", aUser.FullName);
                    aHashtable.Add("FatherName", aUser.FatherName);
                    aHashtable.Add("MotherName", aUser.MotherName);
                    aHashtable.Add("CellNumber", aUser.PhoneNumber);
                    aHashtable.Add("BirthDay", aUser.BirthDate);
                    aHashtable.Add("CurrentAddress", aUser.CurrentAddress);
                    aHashtable.Add("PermanentAddress", aUser.PermanentAddress);
                    aHashtable.Add("BirthCertificateID", aUser.BirthCertificateID);



                    aGateway.Insert(usersQuery, aHashtable);

                    aGateway.Insert(userInfoQuery, aHashtable);

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, aUser);
                    return response;
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/<controller>/5

        public HttpResponseMessage Delete(int id)
        {
            string query = "DELETE FROM `tikaappdb`.`userinfo` WHERE  `ID`=" + id + ";";
            aGateway.Delete(query);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, id);

            return response;
        }
    }
}