using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentProviders_Web_API.DbContexts;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;

namespace PaymentProviders_Web_API.WebApiControllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class PaymentProviderApiController : ControllerBase
    {
        private PaymentProvidersContext providersContext;

        public PaymentProviderApiController()
        {
            providersContext = new PaymentProvidersContext();
        }

        // GET: v1/PaymentProviderApi/OW||COMMUNAL
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<IEnumerable<PaymentProvider>> Get(string id, int? regionId)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            List<PaymentProvider> providers;
            var providersQuery = providersContext.PaymentProviders
                .Where(x => x.CatalogCode == id)
                .Include(x => x.Category)
                .Include(x => x.Fields)
                    .ThenInclude(x => x.MaskListItem)
                .Include(x => x.PaymentInfo)
                    .ThenInclude(x => x.ProductsPaymentInfo)
                    .ThenInclude(x => x.Commission);

            if (regionId.HasValue)
            {
                providers = providersQuery.Where(x => x.Regions.Any(z => z.PaymentRegion.Code == regionId)).ToList();
            }
            else
            {
                providers = providersQuery.ToList();
            }

            if (providers == null || providers.Count == 0)
                return NotFound();
            else
                return providers;
        }
    }
}
