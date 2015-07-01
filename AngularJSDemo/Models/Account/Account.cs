using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using DigitalVaccination.Libs;

namespace DigitalVaccination.Models
{
    public class Account
    {
        DBGateway aGateway = new DBGateway();
        public string AddUser()
        {
            return "";
        }

        public string DeleteUser()
        {
            return "";
        }

       
        public Dictionary<string, string> GetCurrentUserInfo()
        {
            Dictionary<string,string> userInfo = new Dictionary<string, string>();


            return userInfo;
        }
        public List<Dictionary<string, string>> GetAllUserInfo()
        {
            List<Dictionary<string,string>> userInfoList  = new List<Dictionary<string, string>>();


            return userInfoList;
        }

        public string RegisterUser(string childName, string birthDay, string fathersName, string mothersName, string currentAddress, 
            string permanentAddress, string cellNumber, string birthCertificateId, string emailAddress, string repeatEmailAddress, out bool success)
        {
            string message = string.Empty;
            if (!IsValidEmailAddress(emailAddress))
            {
                success = false;
                return "Email address not valid";
            }

            if (emailAddress != repeatEmailAddress)
            {
                success = false;
                return "Re-Enter email address correctly";
            }

            
            if (IsUserAndCellNumberCombinationExists(childName,cellNumber))
            {
                success = false;
                return "Child Name and Cell Number already exists.";
            }


            Hashtable conditionTable = new Hashtable();
            conditionTable.Add("userName", childName);
            conditionTable.Add("cellNumber", cellNumber);

            string insertString = "insert into users (UserName,UserCellNumber,UserRoleId) values (@userName,@cellNumber,'4');select last_insert_id();";

            int insertId;
            message = aGateway.Insert(insertString,conditionTable,out insertId);
            conditionTable.Add("userId", insertId);
            conditionTable.Add("fullName", childName);
            conditionTable.Add("fatherName", fathersName);
            conditionTable.Add("motherName", mothersName);
            conditionTable.Add("birthDay", birthDay);
            
            conditionTable.Add("currentAddress", currentAddress);
            conditionTable.Add("permanentAddress", permanentAddress);
           
            conditionTable.Add("emailAddress", emailAddress);
            conditionTable.Add("repeatEmailAddress", repeatEmailAddress);
            conditionTable.Add("birthCertificateId", birthCertificateId);

            insertString = "insert into userinfo (UserId,UserName,FullName,FatherName,MotherName," +
                                          "CellNumber,BirthDay,CurrentAddress,PermanentAddress,BirthCertificateID" +
                                          ") values (@userId,@userName,@fullName,@fatherName,@motherName," +
                                          "@cellNumber,@birthDay,@currentAddress,@permanentAddress,@birthCertificateID);";

            aGateway.Insert(insertString, conditionTable,out insertId);

            message = "User successfully added.";



            success = true;
            return message;
        }

        private bool IsUserAndCellNumberCombinationExists(string childName, string cellNumber)
        {   
            string query = "select * from users where UserName=@userName and UserCellNumber=@userCellNumber;";
            Hashtable conditionTable = new Hashtable();
            conditionTable.Add("userName",childName);
            conditionTable.Add("userCellNumber", cellNumber);
            DataSet aSet = aGateway.Select(query, conditionTable);
            if (aSet.Tables[0].Rows.Count != 0)
            {
                return true;
            }
            return false;

        }

        private bool IsValidEmailAddress(string emailAddress)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(emailAddress);
            if (match.Success)
            {
                return true;
            }
               
            return false;
        }
    }
}