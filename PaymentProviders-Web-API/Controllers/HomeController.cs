using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PaymentProviders_Web_API.Models;

namespace PaymentProviders_Web_API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Test();
            return View();
        }

        private void Test()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "res", "CodesAndRegions.json");
            JObject json = JObject.Parse(System.IO.File.ReadAllText(filePath));

            var regions = from region in json["data"] select (string)region["name"];

            foreach (var item in regions)
                Debug.WriteLine(item);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
