using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace DAPS.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient  client = new HttpClient();

        public ActionResult Index()
        {
            //communicate with API for the request
            HttpWebRequest apiRequest = WebRequest.CreateHttp("https://api.zinc.io/v1/orders");
            apiRequest.Headers.Add("Authorization", ConfigurationManager.AppSettings["apizinc"]); //used to add keys

            //tell server who is using/client type
            //apiRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)";

            //response from request call
            HttpWebResponse apiResponse = (HttpWebResponse)apiRequest.GetResponse();
            if (apiResponse.StatusCode == HttpStatusCode.OK) //http error 200
            {
                //get data and parse
                StreamReader responseData = new StreamReader(apiResponse.GetResponseStream());

                //save string response back to a var
                string info = responseData.ReadToEnd();

                //add class JOjbect data structure to display better
                JObject jsoninfo = JObject.Parse(info);

                ViewBag.zinc = jsoninfo.ToString();
            }

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