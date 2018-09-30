using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PaymentProviders_Web_API.Managers;
using PaymentProviders_Web_API.Models;
using PaymentProviders_Web_API.Services.Parsers;

namespace PaymentProviders_Web_API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            ProvidersParser parser = new ProvidersParser(filePath);

            var regions = parser.LoadRegions();
            var categories = parser.ParseCategories();
            var providers = parser.ParseProviders();

            /*PaymentProviderDBManager providerDBManager = new PaymentProviderDBManager();

            providerDBManager.SaveRegions(regions);
            providerDBManager.SaveCategories(categories);
            providerDBManager.SaveProviders(providers);*/
            
            return Redirect("/Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
