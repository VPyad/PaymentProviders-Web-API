using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    [DataContract]
    public class PaymentProvider
    {
        public long Id { get; set; }

        /// <summary>
        /// Use as Provider Id
        /// </summary>
        [DataMember]
        public string ProviderCode { get; set; }

        [DataMember]
        public string NameRu { get; set; }

        public long? PaymentInfoId { get; set; }
        [DataMember]
        public virtual PaymentInfo PaymentInfo { get; set; }

        [DataMember]
        public string CatalogCode { get; set; }

        public long? CategoryId { get; set; }
        [DataMember]
        public virtual PaymentCategory Category { get; set; }

        public virtual ICollection<PaymentProviderRegion> Regions { get; set; }

        /// <summary>
        /// Determines whether provider is active of not. If provider was deleted no actions can be permofmed.
        /// </summary>
        [DataMember]
        public bool Deleted { get; set; }

        [DataMember]
        public bool Mrlist { get; set; }

        [DataMember]
        public bool MultiCheck { get; set; }

        [DataMember]
        public bool NoSavePt { get; set; }

        [DataMember]
        public bool Check { get; set; }

        [DataMember]
        public bool IsSupportRequestRSTEP { get; set; }

        [DataMember]
        public long Order { get; set; }

        [DataMember]
        public string ChequeName { get; set; }

        public string RegionString { get; set; }

        [DataMember]
        public virtual ICollection<ProviderField> Fields { get; set; }
    }
}
