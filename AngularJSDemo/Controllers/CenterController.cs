using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalVaccination.Controllers
{
    public class CenterController : Controller
    {
        //
        // GET: /Center/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditCenter()
        {
            return View();
        }

    }
}
