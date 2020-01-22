# TCMB Currency Service
.Net Core 2.1 library project which serve TCMB currency data.  
  * Language Code: C#  
  * [Fluent Interface Pattern](https://en.wikipedia.org/wiki/Fluent_interface "Wikipedia") is implemented on this project.  
  * Test project implemented on Xunit.

## Usage

```c#
CurrencyService _currencyService = new CurrencyService();
var currencyList = _currencyService.Initialize(inputDate).GetList();//inputDate is optional. 
//When a currency list is requested for special date, fill the inputDate param
var USDCurrency  = _currencyService.Initialize().Filter(x => x.Code.Equals("USD")).GetFirst();
var NameStartSCurrencyList = _currencyService.Initialize().Order(x => x.Code, SortOrder.Descending)//result list
                .Filter(x => x.Code.StartsWith("S")).GetList();
var export = _currencyService.Initialize().Filter(x => x.Code.StartsWith("S"))//export xml, json or csv
                .Order(x => x.Code, SortOrder.Ascending).Export(ExportType.Xml);
```

  * The reason I use "Fluent Interface Pattern" is to be able to use sorting and filtering operations easily.  
  * When you initialize the service , you can do sort or filter action on currency list.  
  * When you want to get result list , then call GetList() function after the sort, filter or initialize command.  
  * If you want to export result like Json, Xml or Csv , then call Export(ExportType parameter) function after the sort, filter or initialize command.  
  * If you send public holiday on Turkey or weekend(Saturday and Sunday) date  , you will get exception like **"The date specified may be a weekend or a public holiday!"**  


## Instruction for testing
Execute this command on cmd for running all test
```
 dotnet test .\TCMBCurrencyService\Test\Test.csproj
```


#### Currency Class

```c#
public class Currency
{
        public string Name { get; }
        public string Code { get; }
        public string CrossRateName { get; }
        public double ForexBuying { get; }
        public double ForexSelling { get; }
        public double BanknoteBuying { get; }
        public double BanknoteSelling { get; }
        public double CrossRateUSD { get; }
        public double CrossRateOther { get; }
}
```
