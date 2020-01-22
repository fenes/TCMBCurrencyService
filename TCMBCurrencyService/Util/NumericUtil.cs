using System;
using System.Globalization;

namespace TCMBCurrencyService.Util
{
    public static class NumericUtil
    {
        public static NumberFormatInfo NumberFormatInfo = new NumberFormatInfo
        {
            NumberDecimalSeparator = "."
        };

        public static double ConvertToDouble(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? 0 : Convert.ToDouble(str.Replace('.', ','));
        }
    }
}