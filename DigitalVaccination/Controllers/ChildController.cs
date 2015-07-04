using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalVaccination.Controllers
{
    public class ChildController : Controller
    {
        //
        // GET: /Center/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditChild()
        {
            return View();
        }

    }
}
