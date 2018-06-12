using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    public enum ProductPaymentInfoType
    {
        Account,
        Card,
        Cash,
        InsiderATM,
        OutsiderATM
    }
}
