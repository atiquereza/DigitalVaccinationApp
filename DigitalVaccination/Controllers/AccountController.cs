using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using DigitalVaccination.Libs;
using DigitalVaccination.Models;

namespace DigitalVaccination.Controllers
{
    public class AccountController : Controller
    {

       
        public ActionResult Index()
        {
           return View("LogIn");
        }

        [HttpPost]
        public ActionResult LogIn(string message)
        {
            if (message != null)
            {
                ViewBag.Message = message;
            }
            return View();
        }


        [HttpPost]
        public ActionResult Index(string userName, string cellNumber)
        {
            try
            {
                Session.Clear();
                if (Authentication.Authenticate(userName, cellNumber, Session))
                {
                    return RedirectToAction("Index", "Home");
                }

                return View("LogIn");
            }
            catch(Exception ex)
            {
                ViewData["Message"] = "An Error occured while porcess your request. Error: "+ex.Message;
                return View("Error");
            }
        }
       
        public ActionResult LogOut()
        {
            
            Session.Clear();
            return Redirect("Index");
        }
        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitRegister(string childName, string birthDay, string fathersName, string mothersName, string currentAddress, string permanentAddress, string cellNumber, string birthCertificateId, string emailAddress, string repeatEmailAddress)
        {
            Account account = new Account();
            bool success;
            string message = account.RegisterUser(childName, birthDay,fathersName, mothersName, currentAddress, permanentAddress, cellNumber, birthCertificateId, emailAddress, repeatEmailAddress, out success);

            if (success)
            {
                string url = UtilityLibrary.GetBaseURL() + "Home/Index";
                return RedirectToAction("Index", "Home");
            }

            ViewData["Message"] = message;
            return View("Register");

        }

    }
}
