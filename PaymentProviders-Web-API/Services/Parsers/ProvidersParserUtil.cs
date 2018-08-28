using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProviders_Web_API.Services.Parsers
{
    public class ProvidersParserUtil
    {
        public static void LogFieldsTypeStat(IEnumerable<PaymentProvider> providers)
        {
            Debug.WriteLine($"Providers with at least one field type Int: {providers.Count(x => x.Fields.Any(z => z.Type == Models.WebApi.PaymentsProviders.FieldType.Int))}");
            Debug.WriteLine($"Providers with at least one field type String: {providers.Count(x => x.Fields.Any(z => z.Type == Models.WebApi.PaymentsProviders.FieldType.String))}");
            Debug.WriteLine($"Providers with at least one field type Numeric: {providers.Count(x => x.Fields.Any(z => z.Type == Models.WebApi.PaymentsProviders.FieldType.Numeric))}");
            Debug.WriteLine($"Providers with at least one field type MaskList: {providers.Count(x => x.Fields.Any(z => z.Type == Models.WebApi.PaymentsProviders.FieldType.MaskList))}");
            Debug.WriteLine($"Providers with at least one field type Unknown: {providers.Count(x => x.Fields.Any(z => z.Type == Models.WebApi.PaymentsProviders.FieldType.Unknown))}");

            Debug.WriteLine($"Providers with at least one Interface type Number: {providers.Count(x => x.Fields.Any(z => z.InterfaceType == Models.WebApi.PaymentsProviders.FieldInerfaceType.Number))}");
            Debug.WriteLine($"Providers with at least one Interface type String: {providers.Count(x => x.Fields.Any(z => z.InterfaceType == Models.WebApi.PaymentsProviders.FieldInerfaceType.String))}");
            Debug.WriteLine($"Providers with at least one Interface type Unknown: {providers.Count(x => x.Fields.Any(z => z.InterfaceType == Models.WebApi.PaymentsProviders.FieldInerfaceType.Unknown))}");
        }

        public static void LogProviderAndFiledNameWithTypeFilter(IEnumerable<PaymentProvider> providers, FieldType fieldType)
        {
            foreach (var provider in providers)
            {
                var fields = provider.Fields;
                var filteredFields = fields.Where(x => x.Type == fieldType);

                if (filteredFields != null)
                {
                    Debug.WriteLine($"Provider: {provider.NameRu}");
                    foreach(var item in filteredFields)
                        Debug.WriteLine($"Field name: {item.Name}");
                    Debug.WriteLine("-------------");
                }
            }
        }
    }
}
