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
    public class PaymentRegionApiController : ControllerBase
    {
        private PaymentProvidersContext providersContext;

        public PaymentRegionApiController()
        {
            providersContext = new PaymentProvidersContext();
        }

        // GET: v1/PaymentRegionApi
        [HttpGet]
        public ActionResult<IEnumerable<PaymentRegion>> Get(string q)
        {
            List<PaymentRegion> regions;

            if (string.IsNullOrEmpty(q))
            {
                regions = providersContext.PaymentRegions.ToList();
            }
            else
            {
                regions = providersContext.PaymentRegions.Where(x => x.Name.ToLower().Contains(q.ToLower())).ToList();
            }

            if (regions == null || regions.Count == 0)
                return NotFound();
            else
                return regions;
        }
    }
}
