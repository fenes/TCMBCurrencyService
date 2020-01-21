namespace TCMBCurrencyService.Model
{
    public class Currency
    {
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

        public string Name { get; }
        public string Code { get; }
        public string CrossRateName { get; }
        public double ForexBuying { get; }
        public double ForexSelling { get; }
        public double BanknoteBuying { get; }
        public double BanknoteSelling { get; }
        public double CrossRateUSD { get; }
        public double CrossRateOther { get; }

        public override string ToString()
        {
            return
                $"Name:{Name}, Code:{Code}, CrossRateName:{CrossRateName}, ForexBuying:{ForexBuying}, ForexSelling:{ForexSelling}, " +
                $"BanknoteBuying:{BanknoteBuying}, BanknoteSelling:{BanknoteSelling}, CrossRateUSD:{CrossRateUSD}, CrossRateOther:{CrossRateOther}";
        }

    }
}