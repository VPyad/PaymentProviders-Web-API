using Microsoft.EntityFrameworkCore;
using PaymentProviders_Web_API.DbContexts;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Managers
{
    public class PaymentProviderDBManager : IDisposable
    {
        private PaymentProvidersContext paymentProvidersContext;

        public PaymentProviderDBManager()
        {
            paymentProvidersContext = new PaymentProvidersContext();
        }

        public void SaveRegions(IEnumerable<PaymentRegion> regions)
        {
            foreach (var region in regions)
            {
                paymentProvidersContext.PaymentRegions.Add(region);
            }

            paymentProvidersContext.SaveChanges();
        }

        public void SaveCategories(IEnumerable<PaymentCategory> categories)
        {
            foreach (var category in categories)
            {
                paymentProvidersContext.Categories.Add(category);
            }

            paymentProvidersContext.SaveChanges();
        }

        public void SaveProviders(IEnumerable<PaymentProvider> paymentProviders)
        {
            List<PaymentProvider> providers = new List<PaymentProvider>(paymentProviders);

            foreach (var provider in providers)
            {
                var category = paymentProvidersContext.Categories.Where(x => x.CategoryCode == provider.CatalogCode).FirstOrDefault();
                
                if (category != null)
                {
                    provider.Category = category;
                    provider.CategoryId = category.Id;
                }

                paymentProvidersContext.PaymentProviders.Add(provider);
                paymentProvidersContext.SaveChanges();

                // Region field may be null, in that case does not save ProviderPaymentRegion
                if (string.IsNullOrEmpty(provider.RegionString))
                    continue;

                var regionsArray = provider.RegionString.Split(';');
                var paymentRegions = paymentProvidersContext.PaymentRegions.Where(x => regionsArray.Contains(x.Code.ToString()));
                var providerFromDb = paymentProvidersContext.PaymentProviders.Last();

                foreach (var region in paymentRegions)
                {
                    var paymentProviderRegion = new PaymentProviderRegion
                    {
                        PaymentProvider = providerFromDb,
                        PaymentRegion = region,
                        PaymentRegionId = region.Id,
                        PaymentProviderId = providerFromDb.Id
                    };

                    
                    paymentProvidersContext.PaymentProviderRegions.Add(paymentProviderRegion);
                    providerFromDb.Regions.Add(paymentProviderRegion);
                }

                paymentProvidersContext.SaveChanges();
            }
        }

        public void PrintFirst10Providers()
        {
            var providers = paymentProvidersContext.PaymentProviders
                .Include(x => x.Category)
                .Include(x => x.Fields)
                .Include(x => x.PaymentInfo).ThenInclude(z => z.ProductsPaymentInfo)
                .Take(10);

            foreach (var item in providers)
            {
                Debug.WriteLine(item.NameRu);
                Debug.WriteLine(item.Category.NameRu);
                Debug.WriteLine(item.PaymentInfo.ProductsPaymentInfo.First().ProductType);
                Debug.WriteLine(item.Fields.First().Title);
                Debug.WriteLine("----------------");
            }
        }

        public void PrintProvidersInCategory()
        {
            var categories = paymentProvidersContext.Categories
                .Include(x => x.Providers)
                .Take(10);

            foreach (var item in categories)
            {
                Debug.WriteLine(item.NameRu);
                foreach (var provider in item.Providers)
                {
                    Debug.WriteLine(provider.NameRu);
                }
                Debug.WriteLine("---------------");
            }
        }

        public void Dispose()
        {
            paymentProvidersContext.Dispose();
        }
    }
}
