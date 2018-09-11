using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    public class ProductPaymentInfo
    {
        public long Id { get; set; }

        public ProductPaymentInfoType ProductType { get; set; }

        public virtual Commission Commission { get; set; }

        public double MinSum { get; set; }

        public double MaxSum { get; set; }
    }
}
