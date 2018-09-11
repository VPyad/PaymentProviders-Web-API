using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    public class ProviderField
    {
        public long Id { get; set; }

        public FieldType Type { get; set; }

        public FieldInerfaceType InterfaceType { get; set; }

        public virtual ICollection<ProviderMaskListItem> MaskListItem { get; set; }

        public string Name { get; set; }

        public bool Required { get; set; }

        public int Direction { get; set; }

        public bool DontShow { get; set; }

        public string Mask { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public int? MinLength { get; set; }

        public int? MaxLength { get; set; }

        public string RegExp { get; set; }

        public bool DontTicket { get; set; }
    }

    public enum FieldType
    {
        String,
        Int,
        Numeric,
        MaskList,
        Unknown
    }

    public enum FieldInerfaceType
    {
        Number,
        String,
        Unknown
    }
}
