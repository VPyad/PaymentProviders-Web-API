using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    [DataContract]
    public class ProductPaymentInfo
    {
        public long Id { get; set; }

        [DataMember]
        public ProductPaymentInfoType ProductType { get; set; }

        [DataMember]
        public virtual Commission Commission { get; set; }

        [DataMember]
        public double MinSum { get; set; }

        [DataMember]
        public double MaxSum { get; set; }

        public long? PaymentInfoId { get; set; }

        public virtual PaymentInfo PaymentInfo { get; set; }
    }
}
