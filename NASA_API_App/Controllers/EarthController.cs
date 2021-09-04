using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace NASA_API_App.Controllers
{
    public class EarthController : Controller
    {
        public ActionResult Earth()
        {
            var earthModel = new Models.View_Models.Earth();

            // Put code here to reach out to NASA API and get NEO data, store in earthModel.NEO
            // Also remove original Models library completely from git, and rename new one to just Models

            return View(earthModel);
        }
    }
}