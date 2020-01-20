using System;

namespace TCMBCurrencyService.Util
{
    public static class DateUtil
    {
        public static DateTime GetPreviousFriday(this DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Friday) date = date.AddDays(-1);
            return date;
        }

        public static bool IsHoliday(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}