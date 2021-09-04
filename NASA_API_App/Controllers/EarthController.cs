using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Models;
using Newtonsoft.Json;

namespace NASA_API_App.Controllers
{
    public class EarthController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

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
    }
}