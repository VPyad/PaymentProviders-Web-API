﻿using Newtonsoft.Json.Linq;
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

        public string Test()
        {
            return (string)xdoc.Descendants("Operator").First().Attribute("Name_RU").Value;
        }

        public static IEnumerable<ProviderMaskListItem> ParseMaskListItem(string mask)
        {
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
    }
}
