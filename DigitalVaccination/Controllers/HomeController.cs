using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalVaccination.Models;
using DigitalVaccination.Libs;

namespace DigitalVaccination.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {

            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Account");

            }

            Dictionary<string, string> sessionData = SessionHandler.GetSessionData(Session);
            if (Authentication.Authenticate(sessionData, Session))
            {
                return View();
            }

            return RedirectToAction("Index", "Account");

        }
    }
}
