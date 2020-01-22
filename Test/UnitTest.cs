using System;
using System.Collections.Generic;
using System.Linq;
using TCMBCurrencyService;
using TCMBCurrencyService.Model;
using TCMBCurrencyService.Util;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class UnitTest
    {
        public UnitTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _currencyService = new CurrencyService();
        }

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly CurrencyService _currencyService;

        public static IEnumerable<object[]> TestDateData =>
            new[]
            {
                new object[] {new DateTime(2020, 1, 19), false},
                new object[] {DateTime.Now.AddDays(1), false},
                new object[] {new DateTime(2020, 1, 20), true},
                new object[] {new DateTime(2020, 1, 1), false},
                new object[] {new DateTime(2019, 10, 29), false},
                new object[] {new DateTime(2020, 10, 20), false}
            };

        [Theory]
        [MemberData(nameof(TestDateData))]
        public void TestDate(DateTime input, bool expectedResult)
        {
            if (!expectedResult)
            {
                Assert.Throws<Exception>(() => _currencyService.Initialize(input).GetList());
            }
            else if (input.DayOfWeek == DayOfWeek.Saturday || input.DayOfWeek == DayOfWeek.Sunday ||
                     input.Date > DateTime.Now.Date)
            {
                Assert.Throws<Exception>(() => _currencyService.Initialize(input).GetList());
            }
            else
            {
                var currencyList = _currencyService.Initialize(input).GetList();
                Assert.NotNull(currencyList);
            }
        }

        [Fact]
        public void TestExportCsv()
        {
            var result = _currencyService.Initialize().Filter(x => x.Code.StartsWith("S"))
                .Order(x => x.Code, SortOrder.Ascending).Export(ExportType.Csv);
            _testOutputHelper.WriteLine(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void TestExportJson()
        {
            var result = _currencyService.Initialize().Filter(x => x.Code.StartsWith("S"))
                .Order(x => x.Code, SortOrder.Ascending).Export(ExportType.Json);
            _testOutputHelper.WriteLine(result);
            var isValid = JsonUtil.IsValidJson(result);
            Assert.True(isValid);
        }

        [Fact]
        public void TestExportXml()
        {
            var result = _currencyService.Initialize().Filter(x => x.Code.StartsWith("S"))
                .Order(x => x.Code, SortOrder.Ascending).Export(ExportType.Xml);
            _testOutputHelper.WriteLine(result);
            var isValid = XmlUtil.IsValidXml(result);
            Assert.True(isValid);
        }

        [Fact]
        public void TestFilter()
        {
            var currencyList = _currencyService.Initialize().Filter(x => x.Code.Equals("USD")).GetFirst();
            Assert.NotNull(currencyList);
        }

        [Fact]
        public void TestFilterAfterSort()
        {
            var SEKCurrency = _currencyService.Initialize().Order(x => x.Code, SortOrder.Descending)
                .Filter(x => x.Code.StartsWith("S")).GetFirst()?.Code;
            Assert.Equal("SEK", SEKCurrency);
        }

        [Fact]
        public void TestInit()
        {
            var currencyList = _currencyService.Initialize().GetList();
            Assert.NotNull(currencyList);
        }

        [Fact]
        public void TestMultiFieldFilter()
        {
            var swedishCurrency = _currencyService.Initialize()
                .Filter(x => x.Code.StartsWith("S") && x.Name.StartsWith("SWE")).GetFirst();
            Assert.NotNull(swedishCurrency);
        }

        [Fact]
        public void TestSort()
        {
            var firstCurrency = _currencyService.Initialize().Order(x => x.Code, SortOrder.Ascending).GetList()
                .FirstOrDefault()?.Code;
            Assert.Equal("AUD", firstCurrency);
        }

        [Fact]
        public void TestSortAfterFilter()
        {
            var firstCurrency = _currencyService.Initialize().Filter(x => x.Code.StartsWith("S"))
                .Order(x => x.Code, SortOrder.Ascending).GetList().FirstOrDefault()?.Code;
            Assert.Equal("SAR", firstCurrency);
        }
    }
}