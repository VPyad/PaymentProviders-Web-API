using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    public class Commission
    {
        public long Id { get; set; }

        public bool IsAbsolute { get; set; }

        public double CommissionValue { get; set; }

        public double MinSum { get; set; }

        public double MaxSum { get; set; }
    }
}
