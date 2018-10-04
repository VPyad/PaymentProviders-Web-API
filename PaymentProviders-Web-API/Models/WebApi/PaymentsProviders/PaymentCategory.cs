using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    [DataContract]
    public class PaymentCategory
    {
        public long Id { get; set; }

        /// <summary>
        /// Use as Category Id
        /// </summary>
        [DataMember]
        public string CategoryCode { get; set; }

        [DataMember]
        public string NameRu { get; set; }

        public virtual ICollection<PaymentProvider> Providers { get; set; }
    }
}
