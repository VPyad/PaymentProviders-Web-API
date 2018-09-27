using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    public class PaymentProvider
    {
        public long Id { get; set; }

        /// <summary>
        /// Use as Provider Id
        /// </summary>
        public string ProviderCode { get; set; }

        public string NameRu { get; set; }

        public long? PaymentInfoId { get; set; }
        public virtual PaymentInfo PaymentInfo { get; set; }

        public string CatalogCode { get; set; }

        public long? CategoryId { get; set; }
        public virtual PaymentCategory Category { get; set; }

        public virtual ICollection<PaymentProviderRegion> Regions { get; set; }

        /// <summary>
        /// Determines whether provider is active of not. If provider was deleted no actions can be permofmed.
        /// </summary>
        public bool Deleted { get; set; }

        public bool Mrlist { get; set; }

        public bool MultiCheck { get; set; }

        public bool NoSavePt { get; set; }

        public bool Check { get; set; }

        public bool IsSupportRequestRSTEP { get; set; }

        public long Order { get; set; }

        public string ChequeName { get; set; }

        public string RegionString { get; set; }

        public virtual ICollection<ProviderField> Fields { get; set; }
    }
}
