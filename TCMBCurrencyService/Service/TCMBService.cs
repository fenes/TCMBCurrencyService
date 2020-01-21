using System;
using System.Collections.Generic;
using System.Xml;
using TCMBCurrencyService.Model;
using TCMBCurrencyService.Util;

namespace TCMBCurrencyService.Service
{
    public static class TCMBService
    {
        private const string TRY = "TRY";
        private const string TODAY_XML_URL = "https://www.tcmb.gov.tr/kurlar/today.xml";

        public static List<Currency> GetCurrentCurrencyRates(DateTime? date)
        {
            try
            {
                return date.HasValue ? GetCurrencyRatesWithDate(date.Value) : GetCurrencyRates(TODAY_XML_URL);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static List<Currency> GetCurrencyRatesWithDate(DateTime date)
        {
            try
            {
                if (date.Date > DateTime.Today) throw new Exception("The date cannot be greater than today!");
                if (date.IsHoliday()) throw new Exception("The date specified may be a weekend or a public holiday!");
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

        private static List<Currency> GetCurrencyRates(string Link)
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

        private static List<Currency> ParseXmlDocument(XmlDocument myxml)
        {
            try
            {
                var nameList = myxml.SelectNodes("/Tarih_Date/Currency/CurrencyName");
                var codeList = myxml.SelectNodes("/Tarih_Date/Currency/@CurrencyCode");
                var forexBuyingList = myxml.SelectNodes("/Tarih_Date/Currency/ForexBuying");
                var forexSellingList = myxml.SelectNodes("/Tarih_Date/Currency/ForexSelling");
                var banknoteBuyingList = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteBuying");
                var banknoteSellingList = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteSelling");

                var ExchangeRates = new List<Currency>
                {
                    new Currency("Turkish Lira", TRY, TRY + "/" + TRY, 1, 1, 1, 1)
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

                    ExchangeRates.Add(cur);
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