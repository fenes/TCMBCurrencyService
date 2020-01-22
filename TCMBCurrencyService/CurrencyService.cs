using System;
using System.Collections.Generic;
using System.Linq;
using TCMBCurrencyService.Interface;
using TCMBCurrencyService.Model;
using TCMBCurrencyService.Service;
using TCMBCurrencyService.Util;

namespace TCMBCurrencyService
{
    public sealed class CurrencyService : IInitializeOperationAfter, IOrderOperationAfter, IFilterOperationAfter
    {
        private IEnumerable<Currency> CurrencyList { get; set; }

        public IFilterOperationAfter Filter(Func<Currency, bool> expression)
        {
            CurrencyList = CurrencyList.Where(expression);
            return this;
        }

        public IOrderOperationAfter Order(Func<Currency, object> expression, SortOrder sortOrder)
        {
            CurrencyList = sortOrder == SortOrder.Ascending
                ? CurrencyList.OrderBy(expression)
                : CurrencyList.OrderByDescending(expression);
            return this;
        }

        public string Export(ExportType exportType)
        {
            switch (exportType)
            {
                case ExportType.Xml:
                    return CurrencyList.ToArray().ToXML();//must serialize
                case ExportType.Json:
                    return CurrencyList.ToJson();
                case ExportType.Csv:
                    return CurrencyList.ToCsv();
                default:
                    throw new ArgumentOutOfRangeException(nameof(exportType), exportType, "Wrong Export type");
            }
        }

        public List<Currency> GetList()
        {
            return CurrencyList.ToList();
        }

        public Currency GetFirst()
        {
            return CurrencyList.FirstOrDefault();
        }

        public IInitializeOperationAfter Initialize(DateTime? date = null)
        {
            CurrencyList = TCMBService.GetCurrentCurrencyRates(date);
            return this;
        }
    }
}