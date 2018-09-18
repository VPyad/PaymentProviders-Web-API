using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    public class ProviderMaskListItem
    {
        public long Id { get; set; }

        public string Key { get; set; }

        public string Desc { get; set; }

        public long ProviderFieldId { get; set; }

        public virtual ProviderField ProviderField { get; set; }
    }
}
