using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    [DataContract]
    public class PaymentInfo
    {
        public long Id { get; set; }

        [DataMember]
        public virtual ICollection<ProductPaymentInfo> ProductsPaymentInfo { get; set; }

        public long PaymentProviderRef { get; set; }

        public virtual PaymentProvider PaymentProvider { get; set; }
    }
}
