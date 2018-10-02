using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    [DataContract]
    public class ProviderMaskListItem
    {
        public long Id { get; set; }

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Desc { get; set; }

        public long ProviderFieldId { get; set; }

        public virtual ProviderField ProviderField { get; set; }
    }
}
