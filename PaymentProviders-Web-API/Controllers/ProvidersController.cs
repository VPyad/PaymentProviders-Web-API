using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentProviders_Web_API.Managers;
using PaymentProviders_Web_API.Services.Parsers;

namespace PaymentProviders_Web_API.Controllers
{
    public class ProvidersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            PaymentProviderDBManager manager = new PaymentProviderDBManager();

            manager.PrintProvidersInCategory();

            return Ok();

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

            PaymentProviderDBManager providerDBManager = new PaymentProviderDBManager();

            providerDBManager.SaveCategories(categories);
            providerDBManager.SaveProviders(providers);

            return Ok(new { Name = file.FileName, Length = file.Length, Path = filePath });
        }
    }
}