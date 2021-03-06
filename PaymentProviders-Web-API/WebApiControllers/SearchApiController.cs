﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PaymentProviders_Web_API.DbContexts;
using PaymentProviders_Web_API.Helpers;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;

namespace PaymentProviders_Web_API.WebApiControllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class SearchApiController : ControllerBase
    {
        private PaymentProvidersContext providersContext;

        public SearchApiController()
        {
            providersContext = new PaymentProvidersContext();
        }

        [HttpGet]
        public ActionResult Get(string q, int? regionId, string filter)
        {
            if (string.IsNullOrEmpty(q))
                return BadRequest(new { Message = "Search term is null or empty" });

            if (filter == null)
                return ReturnAll(q, regionId);
            else switch (filter.ToLower())
                {
                    case "providers":
                        return ReturnProviders(q, regionId);
                    case "categories":
                        return ReturnCategories(q);
                    default:
                        return BadRequest(new { Message = "filter parameter has invalid value" });
                }
        }

        private List<PaymentCategory> LoadCategories(string q)
        {
            return providersContext.Categories.Where(x => x.NameRu.ToLower().Contains(q.ToLower())).ToList();
        }

        private List<PaymentProvider> LoadProviders(string q, int? regionId)
        {
            var providersQuery = providersContext.PaymentProviders
                .Where(x => x.NameRu.ToLower().Contains(q.ToLower()))
                .Include(x => x.Category)
                .Include(x => x.Fields)
                    .ThenInclude(x => x.MaskListItem)
                .Include(x => x.PaymentInfo)
                    .ThenInclude(x => x.ProductsPaymentInfo)
                    .ThenInclude(x => x.Commission);

            if (regionId.HasValue)
            {
                return providersQuery.Where(x => x.Regions.Any(z => z.PaymentRegion.Code == regionId)).ToList();
            }
            else
            {
                return providersQuery.ToList();
            }
        }

        private ActionResult ReturnProviders(string q, int? regionId)
        {
            List<PaymentProvider> providers;

            try
            {
                providers = LoadProviders(q, regionId);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }

            if (providers == null || providers.Count == 0)
                return NotFound();
            else
                return new JsonResult(new { Providers = providers });
        }

        private ActionResult ReturnCategories(string q)
        {
            List<PaymentCategory> categories;

            try
            {
                categories = LoadCategories(q);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            if (categories == null || categories.Count == 0)
                return NotFound();
            else
                return new JsonResult(new { Categories = categories });
        }

        private ActionResult ReturnAll(string q, int? regionId)
        {
            List<PaymentProvider> providers;
            List<PaymentCategory> categories;

            try
            {
                providers = LoadProviders(q, regionId);
                categories = LoadCategories(q);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            var providersIsNull = providers == null || providers.Count == 0;
            var categoriesIsNull = categories == null || categories.Count == 0;

            if (providersIsNull && categoriesIsNull)
                return NotFound();
            else
                return new JsonResult(new { Providers = providers, Categories = categories },
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new IgnoreEmptyEnumerablesResolver()
                    });
        }
    }
}
