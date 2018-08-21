using Newtonsoft.Json.Linq;
using PaymentProviders_Web_API.Models.WebApi.PaymentsProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PaymentProviders_Web_API.Services.Parsers
{
    public class ProvidersParser
    {
        private XDocument xdoc;

        public ProvidersParser(string pathToFile)
        {
            xdoc = XDocument.Load(pathToFile);
        }

        public IEnumerable<PaymentProvider> ParseProviders()
        {
            var collection = from provider in xdoc.Descendants("Operator")
                             select new PaymentProvider
                             {
                                 ProviderCode = (string)provider.Attribute("OperatorCode"),
                                 NameRu = (string)provider.Attribute("Name_RU"),
                                 Order = (int)provider.Attribute("Order"),
                                 CatalogCode = (string)provider.Attribute("Senior"),
                                 Mrlist = (bool)provider.Attribute("Mrlist"),
                                 MultiCheck = (bool)provider.Attribute("MultiCheck"),
                                 NoSavePt = (bool)provider.Attribute("NoSavePT"),
                                 Check = (bool)provider.Attribute("Check"),
                                 Deleted = (bool)provider.Attribute("Deleted"),
                                 IsSupportRequestRSTEP = (bool)provider.Attribute("IsSupportRequestRSTEP"),
                                 ChequeName = (string)provider.Elements("Localizations").LastOrDefault().Value,
                                 PaymentInfo = new PaymentInfo
                                 {
                                     ProductsPaymentInfo = ParsePaymentInfo((string)provider.Attribute("ExtraCommission"),
                                     (string)provider.Attribute("Summa"), (string)provider.Attribute("MaxSumma"))
                                 },
                                 Regions = ParseRegions((string)provider.Attribute("Region")),
                                 Fields = ParseFields(provider.Elements("Param"))
                             };

            return collection;
        }

        private static IEnumerable<ProviderMaskListItem> ParseMaskListItem(string mask, string type)
        {
            if (type != "MaskList")
                return null;

            List<ProviderMaskListItem> list = new List<ProviderMaskListItem>();

            char pairSeparator = mask[0];
            char keyValSeparator = mask[1];

            string KeyValMask = mask.Remove(0, 2);

            foreach (var pair in KeyValMask.Split(pairSeparator))
            {
                string[] keyVal = pair.Split(keyValSeparator);
                list.Add(new ProviderMaskListItem { Key = keyVal[0], Desc = keyVal[1] });
            }

            return list;
        }

        private static IEnumerable<ProductPaymentInfo> ParsePaymentInfo(string commision, string minSumma, string maxSumma)
        {
            List<ProductPaymentInfo> productPaymInfoList = new List<ProductPaymentInfo>();
                        
            var commisionArray = commision.Split(';');
            var minSummaArray = minSumma.Split(',');
            var maxSummaArray = maxSumma.Split(',');

            // amount of comission object is always 5
            for (int i = 0; i < 5; i++)
            {
                ProductPaymentInfo paymentInfo = new ProductPaymentInfo();

                // amount of min and max summa objects may be less than amount of commision object and may be empty
                if (i <= minSummaArray.Count() - 1 && minSummaArray[i] != "")
                    paymentInfo.MinSum = double.Parse(minSummaArray[i]);

                if (i <= maxSummaArray.Count() - 1 && maxSummaArray[i] != "")
                    paymentInfo.MaxSum = double.Parse(maxSummaArray[i]);

                // product type determine its index
                switch (i)
                {
                    case 0:
                        paymentInfo.ProductType = ProductPaymentInfoType.Cash;
                        break;
                    case 1:
                        paymentInfo.ProductType = ProductPaymentInfoType.OutsiderATM;
                        break;
                    case 2:
                        paymentInfo.ProductType = ProductPaymentInfoType.InsiderATM;
                        break;
                    case 3:
                        paymentInfo.ProductType = ProductPaymentInfoType.Card;
                        break;
                    case 4:
                        paymentInfo.ProductType = ProductPaymentInfoType.Cash;
                        break;
                }
                
                string commisionItem = commisionArray[i];

                // commision may be null. in that case continue loop
                if (string.IsNullOrEmpty(commisionItem))
                {
                    paymentInfo.Commission = null;
                    productPaymInfoList.Add(paymentInfo);
                    continue;
                }

                var commisionItemProps = commisionItem.Split('('); // split commision object into commision value, and range for that commision
                string commsionRawValue = commisionItemProps[0];
                string rangeRawValue = commisionItemProps[1];
                rangeRawValue = rangeRawValue.Remove(rangeRawValue.Length - 1, 1); // get rid of ')'
                Commission commission = new Commission();

                var rangeValue = rangeRawValue.Split('-');
                commission.MinSum = double.Parse(rangeValue[0]);
                commission.MaxSum = double.Parse(rangeValue[1]);

                if (commsionRawValue.Contains("%"))
                {
                    commission.IsAbsolute = false;
                    commsionRawValue = commsionRawValue.Remove(commsionRawValue.IndexOf('%'), 1);
                }
                else
                {
                    commission.IsAbsolute = true;
                }

                commission.CommissionValue = double.Parse(commsionRawValue);
                paymentInfo.Commission = commission;

                productPaymInfoList.Add(paymentInfo);
            }

            return productPaymInfoList;
        }

        private static IEnumerable<PaymentRegion> ParseRegions(string regions)
        {
            if (string.IsNullOrEmpty(regions))
            {
                return null;
            }

            List<PaymentRegion> list = new List<PaymentRegion>();
            var regionsArray = regions.Split(';');

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "res", "CodesAndRegions.json");
            JObject json = JObject.Parse(System.IO.File.ReadAllText(filePath));

            var regionsList = from region in json["data"] select new { Name = (string)region["name"], Code = (string)region["regioncode"] };

            foreach (var item in regionsArray)
                list.Add(new PaymentRegion { Code = int.Parse(item), Name = regionsList.First(x => x.Code == item).Name });

            return list;

        }

        private static IEnumerable<ProviderField> ParseFields(IEnumerable<XElement> fieldsXml)
        {
            var collection = from field in fieldsXml
                             select new ProviderField
                             {
                                 Name = (string)field.Attribute("Name"),
                                 Required = ParseCBool((string)field.Attribute("Required")),
                                 Direction = (int)field.Attribute("Direction"),
                                 DontShow = (bool)field.Attribute("DontShow"),
                                 DontTicket = (bool)field.Attribute("DontTicket"),
                                 MaxLength = (int)field.Attribute("MaxLength"),
                                 MinLength = (int)field.Attribute("MinLength"),
                                 Title = (string)field.Attribute("Title"),
                                 RegExp = (string)field.Attribute("RegularExpression"),
                                 Comment = (string)field.Attribute("Comment"),
                                 InterfaceType = ParseInerfaceType("InterfaceType"),
                                 Type = ParseFieldType("Type"),
                                 Mask = ParseMask((string)field.Attribute("Mask"), (string)field.Attribute("Type")),
                                 MaskListItem = ParseMaskListItem((string)field.Attribute("Mask"), (string)field.Attribute("Type"))
                             };
            return null;
        }

        private static bool ParseCBool(string value)
        {
            return value == "1" ? true : false;
        }

        private static FieldType ParseFieldType(string filedType)
        {
            switch (filedType)
            {
                case "String":
                    return FieldType.String;
                case "Int":
                    return FieldType.Int;
                case "Numeric":
                    return FieldType.Numeric;
                case "MaskList":
                    return FieldType.MaskList;
                default:
                    return FieldType.Unknown;
            }
        }

        private static FieldInerfaceType ParseInerfaceType(string interfaceType)
        {
            switch (interfaceType)
            {
                case "%String":
                    return FieldInerfaceType.String;
                case "%Number":
                    return FieldInerfaceType.Number;
                default:
                    return FieldInerfaceType.Unknown;
            }
        }

        private static string ParseMask(string mask, string type)
        {
            if (type == "MaskList" || mask == "true")
                return "";
            else
                return mask;
        }
    }
}
