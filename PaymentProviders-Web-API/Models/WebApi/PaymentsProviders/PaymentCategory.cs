using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    public class PaymentCategory
    {
        public long Id { get; set; }

        /// <summary>
        /// Use as Category Id
        /// </summary>
        public string CategoryCode { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }
    }
}
