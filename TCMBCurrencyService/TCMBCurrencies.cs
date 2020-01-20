using System;
using System.Collections.Generic;
using TCMBCurrencyService.Model;
using TCMBCurrencyService.Service.Implementation;
using TCMBCurrencyService.Util;

namespace TCMBCurrencyService
{
    public class TCMBCurrencies
    {
        public Dictionary<string, Currency> GetCurrencyList()
        {
            try
            {
                return CurrencyService.GetCurrentCurrencyRates();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.ToString());
            }
        }

        public Dictionary<string, Currency> GetCurrencyListWithDate(DateTime date)
        {
            try
            {
                if (date.Date > DateTime.Today) throw new Exception("The date cannot be greater than today!");
                if (date.IsHoliday()) throw new Exception("The date specified may be a weekend or a public holiday!");
                return CurrencyService.GetCurrencyRatesWithDate(date);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}