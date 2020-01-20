using System;
using System.Collections.Generic;
using System.Xml;
using TCMBCurrencyService.Model;
using TCMBCurrencyService.Util;

namespace TCMBCurrencyService.Service.Implementation
{
    public static class CurrencyService
    {
        private const string TRY = "TRY";
        private const string TODAY_XML_URL = "https://www.tcmb.gov.tr/kurlar/today.xml";

        public static Dictionary<string, Currency> GetCurrentCurrencyRates()
        {
            try
            {
                return GetCurrencyRates(TODAY_XML_URL);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static Dictionary<string, Currency> GetCurrencyRatesWithDate(DateTime date)
        {
            try
            {
                var requestDate = GetRequestDate(date);
                var url = GenerateUrl(requestDate);
                return GetCurrencyRates(url);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static DateTime GetRequestDate(DateTime date)
        {
            return date.IsHoliday() ? date.GetPreviousFriday() : date;
        }

        private static string GenerateUrl(DateTime requestDate)
        {
            return string.Format("https://www.tcmb.gov.tr/kurlar/{0}/{1}.xml", requestDate.ToString("yyyyMM"),
                requestDate.ToString("ddMMyyyy"));
        }

        private static Dictionary<string, Currency> GetCurrencyRates(string Link)
        {
            var myxml = new XmlDocument(); // Create XmlDocument object.
            try
            {
                var rdr = new XmlTextReader(Link);
                // Create XmlTextReader object with parameter that xml document link .
                // XmlTextReader provides fast and forward-only access to the xml documents specified in urls.
                myxml.Load(rdr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("The date specified may be a weekend or a public holiday!");
            }

            return ParseXmlDocument(myxml);
        }

        private static Dictionary<string, Currency> ParseXmlDocument(XmlDocument myxml)
        {
            try
            {
                var nameList = myxml.SelectNodes("/Tarih_Date/Currency/CurrencyName");
                var codeList = myxml.SelectNodes("/Tarih_Date/Currency/@CurrencyCode");
                var forexBuyingList = myxml.SelectNodes("/Tarih_Date/Currency/ForexBuying");
                var forexSellingList = myxml.SelectNodes("/Tarih_Date/Currency/ForexSelling");
                var banknoteBuyingList = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteBuying");
                var banknoteSellingList = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteSelling");

                var ExchangeRates = new Dictionary<string, Currency>
                {
                    {TRY, new Currency("Turkish Lira", TRY, TRY + "/" + TRY, 1, 1, 1, 1)}
                };


                for (var i = 0; i < nameList.Count; i++)
                {
                    Console.WriteLine(i);
                    var cur = new Currency(nameList.Item(i)?.InnerText,
                        codeList?.Item(i)?.InnerText,
                        codeList?.Item(i).InnerText + "/" + TRY,
                        forexBuyingList.Item(i).InnerText.ConvertToDouble(),
                        forexSellingList.Item(i).InnerText.ConvertToDouble(),
                        banknoteBuyingList.Item(i).InnerText.ConvertToDouble(),
                        banknoteSellingList.Item(i).InnerText.ConvertToDouble()
                    );

                    ExchangeRates.Add(codeList?.Item(i)?.InnerText, cur);
                }

                return ExchangeRates;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}