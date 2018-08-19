using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            string s = parser.Test();

            string test = @";-0-пополнение лицевого счета;2-оплата пакета «Ночной» год;5-оплата пакета «Наш футбол»месяц;16-оплата пакета «Детский» год;21-оплата пакета «Единый»;24-оплата пакета «Мультирум»(500 р./год);27-оплата пакета «Детский» месяц;31-оплата пакета «Ночной» месяц;45-оплата пакета «МАТЧ! ФУТБОЛ» месяц;46-оплата пакета «Сити»;48-оплата пакета «UltraHD»;49-оплата пакета «Спутниковый интернет»;52-оплата пакета «Весь футбол»";
            var list = ProvidersParser.ParseMaskListItem(test);

            foreach (var item in list)
                Debug.WriteLine($"key: {item.Key}, desc: {item.Desc}");

            return Ok(new { Name = file.FileName, Length = file.Length, Path = filePath, text = s });
        }
    }
}