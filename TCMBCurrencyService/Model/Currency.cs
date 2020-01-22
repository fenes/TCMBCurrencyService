using System;
using TCMBCurrencyService.Util;

namespace TCMBCurrencyService.Model
{
    [Serializable]
    public class Currency
    {
        public Currency()
        {
        }

        public Currency(string name, string code, string crossRateName, double forexBuying, double forexSelling,
            double banknoteBuying, double banknoteSelling, double crossRateUsd, double crossRateOther)
        {
            Name = name;
            Code = code;
            CrossRateName = crossRateName;
            ForexBuying = forexBuying;
            ForexSelling = forexSelling;
            BanknoteBuying = banknoteBuying;
            BanknoteSelling = banknoteSelling;
            CrossRateUSD = crossRateUsd;
            CrossRateOther = crossRateOther;
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public string CrossRateName { get; set; }
        public double ForexBuying { get; set; }
        public double ForexSelling { get; set; }
        public double BanknoteBuying { get; set; }
        public double BanknoteSelling { get; set; }
        public double CrossRateUSD { get; set; }
        public double CrossRateOther { get; set; }

        public override string ToString()
        {
            return
                $"{Name}, {Code}, {CrossRateName}, {ForexBuying.ToString(NumericUtil.NumberFormatInfo)}, {ForexSelling.ToString(NumericUtil.NumberFormatInfo)}, " +
                $"{BanknoteBuying.ToString(NumericUtil.NumberFormatInfo)}, {BanknoteSelling.ToString(NumericUtil.NumberFormatInfo)}, {CrossRateUSD.ToString(NumericUtil.NumberFormatInfo)}, {CrossRateOther.ToString(NumericUtil.NumberFormatInfo)}";
        }
    }
}