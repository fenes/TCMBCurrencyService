using System;
using System.Collections.Generic;
using System.Linq;
using TCMBCurrencyService.Action.Implementation;
using TCMBCurrencyService.Model;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class UnitTest
    {
        public UnitTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _currencyAction = new CurrencyAction();
        }

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly CurrencyAction _currencyAction;

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
                Assert.Throws<Exception>(() => _currencyAction.Initialize(input).GetList());
            }
            else if (input.DayOfWeek == DayOfWeek.Saturday || input.DayOfWeek == DayOfWeek.Sunday ||
                     input.Date > DateTime.Now.Date)
            {
                Assert.Throws<Exception>(() => _currencyAction.Initialize(input).GetList());
            }
            else
            {
                var currencyList = _currencyAction.Initialize(input).GetList();
                Assert.NotNull(currencyList);
            }
        }

        [Fact]
        public void TestInit()
        {
            var currencyList = _currencyAction.Initialize().GetList();
            Assert.NotNull(currencyList);
        }

        [Fact]
        public void TestFilter()
        {
            var currencyList = _currencyAction.Initialize().Filter(x => x.Code.Equals("USD")).GetList();
            Assert.NotNull(currencyList);
        }

        [Fact]
        public void TestSort()
        {
            var firstCurrency = _currencyAction.Initialize().Order(x => x.Code, SortOrder.Ascending).GetList().FirstOrDefault().Code;
            Assert.Equal("AUD", firstCurrency);
        }
    }
}