using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentProviders_Web_API.DbContexts;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;

namespace PaymentProviders_Web_API.WebApiControllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class PaymentCategoryApiController : ControllerBase
    {
        private PaymentProvidersContext providersContext;

        public PaymentCategoryApiController()
        {
            providersContext = new PaymentProvidersContext();
        }

        // GET: v1/PaymentCategoryApi
        [HttpGet]
        public ActionResult<IEnumerable<PaymentCategory>> Get()
        {
            var categories = providersContext.Categories.ToList();

            if (categories == null || categories.Count == 0)
                return NotFound();
            else
                return categories;
        }
    }
}
