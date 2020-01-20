using System;

namespace TCMBCurrencyService.Util
{
    public static class NumericUtil
    {
        public static double ConvertToDouble(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? 0 :  Convert.ToDouble(str.Replace('.',','));
        }
    }
}