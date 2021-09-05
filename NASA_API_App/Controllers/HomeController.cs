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
        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
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

            // Return the APOD page with the received data
            return View(APODViewModel);
        }

        // TODO: ADD SEARCH BY DATE
        [HttpPost]
        public ActionResult APOD(DateTime date)
        {
            // Create APOD Object to store response
            var APODViewModel = new Models.View_Models.APODView();

            // Get API Key and URL for the API call
            var APIKey = WebConfigurationManager.AppSettings["NASA_API_KEY"];
            // ADD DATE HERE
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

            // Return the APOD page with the received data
            return View(APODViewModel);
        }

        [HttpGet]
        public ActionResult Earth()
        {
            var earthModel = new Models.View_Models.EarthView();

            // Get API Key and Current Date for the API call
            var APIKey = WebConfigurationManager.AppSettings["NASA_API_KEY"];
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            var nextDay = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");

            var url = "https://api.nasa.gov/neo/rest/v1/feed?start_date=" + currentDate + "&end_date=" + currentDate + "&api_key=" + APIKey;
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            var json = new JsonSerializer();

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                var jsonReader = new JsonTextReader(streamReader);
                //var deserialized = JsonConvert.DeserializeObject<Dictionary<DateTime, BusStop>>(json);
                var data = json.Deserialize<Models.API_Objects.NEORootobject>(jsonReader);
            }




            return View(earthModel);
        }

        [HttpGet]
        public ActionResult Mars()
        {
            var marsModel = new Models.View_Models.MarsView();

            // Get API Key, Recent Date (5 days ago), and Page for the API call
            var APIKey = WebConfigurationManager.AppSettings["NASA_API_KEY"];
            var recentDate = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
            var page = 1;
            var url = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=" + recentDate + "&page=" + page + "&api_key=" + APIKey;

            // Make the API Call and store the result in Photos
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            var json = new JsonSerializer();
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var jsonReader = new JsonTextReader(streamReader);
                marsModel.Photos = json.Deserialize<Models.API_Objects.NASA_Mars_Photos_Object>(jsonReader);
            }

            return View(marsModel);
        }

        [HttpPost]
        // TODO: ADD PAGE FUNCTIONALITY, ALSO ADD SEARCH BY DATE AND FILTER BY CAMERA
        public ActionResult Mars(int page = 1)
        {
            var marsModel = new Models.View_Models.MarsView();

            // Get API Key, Recent Date (5 days ago), and Page for the API call
            var APIKey = WebConfigurationManager.AppSettings["NASA_API_KEY"];
            var recentDate = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
            var url = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=" + recentDate + "&page=" + page + "&api_key=" + APIKey;

            // Make the API Call and store the result in Photos
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            var json = new JsonSerializer();
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var jsonReader = new JsonTextReader(streamReader);
                marsModel.Photos = json.Deserialize<Models.API_Objects.NASA_Mars_Photos_Object>(jsonReader);
            }

            return View(marsModel);
        }
    }
}