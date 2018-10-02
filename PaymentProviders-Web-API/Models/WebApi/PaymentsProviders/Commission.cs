using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    [DataContract]
    public class Commission
    {
        public long Id { get; set; }

        [DataMember]
        public bool IsAbsolute { get; set; }

        [DataMember]
        public double CommissionValue { get; set; }

        [DataMember]
        public double MinSum { get; set; }

        [DataMember]
        public double MaxSum { get; set; }

        public long ProductPaymentInfoRef { get; set; }

        public virtual ProductPaymentInfo ProductPaymentInfo { get; set; }
    }
}
