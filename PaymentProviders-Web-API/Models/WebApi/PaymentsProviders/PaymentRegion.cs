using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    public class PaymentRegion
    {
        public long Id { get; set; }

        public int Code { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PaymentProviderRegion> Providers { get; set; }
    }
}
