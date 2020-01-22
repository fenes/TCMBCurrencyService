using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TCMBCurrencyService.Util
{
    public static class CsvHelper
    {
        public static string ToCsv<T>(this IEnumerable<T> obj)
        {
            var csvHeaderRow = string.Join(",",
                                   typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                       .Select(x => x.Name).ToArray()) + Environment.NewLine;
            var csv = csvHeaderRow + string.Join(Environment.NewLine, obj.Select(x => x.ToString()).ToArray());
            return csv;
        }
    }
}