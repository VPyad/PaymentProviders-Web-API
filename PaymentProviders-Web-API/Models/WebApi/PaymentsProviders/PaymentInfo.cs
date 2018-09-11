using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    public class PaymentInfo
    {
        public long Id { get; set; }

        public virtual ICollection<ProductPaymentInfo> ProductsPaymentInfo { get; set; }
    }
}
