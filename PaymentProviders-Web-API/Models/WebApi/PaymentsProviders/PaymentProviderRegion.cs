using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    public class PaymentProviderRegion
    {
        public long PaymentProviderId { get; set; }

        public PaymentProvider PaymentProvider { get; set; }

        public long PaymentRegionId { get; set; }

        public PaymentRegion PaymentRegion { get; set; }
    }
}
