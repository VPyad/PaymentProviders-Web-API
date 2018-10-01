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
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;
using PaymentProviders_Web_API.Services.Parsers;

namespace PaymentProviders_Web_API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(bool? isSuccess, string importMessage)
        {
            ViewBag.isImportSuccessed = isSuccess;
            ViewBag.importMessage = importMessage;

            return View();
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return RedirectToAction("Index", "Home", new { isSuccess = false, importMessage = "file not selected" });

            var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            ProvidersParser parser = new ProvidersParser(filePath);

            IEnumerable<PaymentRegion> regions;
            IEnumerable<PaymentCategory> categories;
            IEnumerable<PaymentProvider> providers;

            try
            {
                regions = parser.LoadRegions();
                categories = parser.ParseCategories();
                providers = parser.ParseProviders();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home", new { isSuccess = false, importMessage = "parsing input catalog error" });
            }

            if (regions == null || categories == null || providers == null)
            {
                return RedirectToAction("Index", "Home", new { isSuccess = false, importMessage = "necessary data wasn`t parsed" });
            }

            PaymentProviderDBManager providerDBManager = new PaymentProviderDBManager();

            try
            {                
                providerDBManager.CleadDB();

                providerDBManager.SaveRegions(regions);
                providerDBManager.SaveCategories(categories);
                providerDBManager.SaveProviders(providers);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home", new { isSuccess = false, importMessage = "failed to save data to db" });
            }

            return RedirectToAction("Index", "Home", new { isSuccess = true });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
