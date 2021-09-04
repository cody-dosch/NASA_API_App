using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace NASA_API_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult APOD()
        {
            // Create APOD Object to store response
            var APODViewModel = new Models.View_Models.APODView();

            // Get API Key and URL for the API call
            var APIKey = WebConfigurationManager.AppSettings["NASA_API_KEY"];
            var url = "https://api.nasa.gov/planetary/apod?api_key=" + APIKey;

            // Make the API Call and store the result in APOD
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            var json = new JsonSerializer();
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var jsonReader = new JsonTextReader(streamReader);
                APODViewModel.APOD = json.Deserialize<Models.API_Objects.NASA_APOD_Object>(jsonReader);
            }

            return View(APODViewModel);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}