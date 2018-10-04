using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProductPaymentInfoType
    {
        Account,
        Card,
        Cash,
        InsiderATM,
        OutsiderATM
    }
}
