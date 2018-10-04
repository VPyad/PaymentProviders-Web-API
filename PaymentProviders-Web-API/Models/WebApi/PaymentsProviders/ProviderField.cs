using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Models.WebApi.PaymentsProviders
{
    [DataContract]
    public class ProviderField
    {
        public long Id { get; set; }

        [DataMember]
        public FieldType Type { get; set; }

        [DataMember]
        public FieldInerfaceType InterfaceType { get; set; }

        [DataMember]
        public virtual ICollection<ProviderMaskListItem> MaskListItem { get; set; }

        public virtual PaymentProvider PaymentProvider { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool Required { get; set; }

        [DataMember]
        public int Direction { get; set; }

        [DataMember]
        public bool DontShow { get; set; }

        [DataMember]
        public string Mask { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public int? MinLength { get; set; }

        [DataMember]
        public int? MaxLength { get; set; }

        [DataMember]
        public string RegExp { get; set; }

        [DataMember]
        public bool DontTicket { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum FieldType
    {
        String,
        Int,
        Numeric,
        MaskList,
        Unknown
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum FieldInerfaceType
    {
        Number,
        String,
        Unknown
    }
}
