using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    [DataContract]
    public class PaymentRegion
    {
        public long Id { get; set; }

        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Name { get; set; }

        public virtual ICollection<PaymentProviderRegion> Providers { get; set; }
    }
}
