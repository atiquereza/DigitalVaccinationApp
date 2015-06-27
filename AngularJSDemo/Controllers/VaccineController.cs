using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngularJSDemo.Libs;

namespace AngularJSDemo.Controllers
{
    public class VaccineController : Controller
    {
        //
        // GET: /Vaccine/
       // [Authenticate]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}
