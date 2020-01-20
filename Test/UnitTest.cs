using System;
using System.Collections.Generic;
using TCMBCurrencyService;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class UnitTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly TCMBCurrencies _tcmbCurrencies;

        public UnitTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _tcmbCurrencies = new TCMBCurrencies();
        }

        [Fact]
        public void Init()
        {
            var currencyList = _tcmbCurrencies.GetCurrencyList();
            Assert.True(currencyList.ContainsKey("TRY"));
            Assert.NotNull(currencyList);
        }

        public static IEnumerable<object[]> TestDateData =>
            new[]
            {
                new object[] { new DateTime(2020, 1,19), false },
                new object[] { DateTime.Now.AddDays(1), false},
                new object[] { new DateTime(2020, 1,20), true },
                new object[] { new DateTime(2020, 1,1), false },
                new object[] { new DateTime(2019, 10,29), false},
                new object[] { new DateTime(2020, 10,20),false }
            };

        [Theory, MemberData(nameof(TestDateData))]
        public void TestDate(DateTime input, bool expectedResult)
        {

            if (!expectedResult)
                Assert.Throws<Exception>(() => _tcmbCurrencies.GetCurrencyListWithDate(input));
            else if (input.DayOfWeek == DayOfWeek.Saturday || input.DayOfWeek == DayOfWeek.Sunday || input.Date > DateTime.Now.Date)
                Assert.Throws<Exception>(() => _tcmbCurrencies.GetCurrencyListWithDate(input));
            else
            {
                var currencyList = _tcmbCurrencies.GetCurrencyListWithDate(input);
                Assert.NotNull(currencyList);
            }
        }


    }
}