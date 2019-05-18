using System.Collections.Generic;
using System.Linq;
using Whid.Domain.Dates;
using Xunit;

namespace Whid.Domain.Test
{
    public class SummaryTests
    {
        private readonly IEnumerable<Summary> _allDays;
        private readonly IEnumerable<Summary> _allWeeks;
        private readonly IEnumerable<Summary> _allMonths;

        public SummaryTests()
        {
            var startDate = new Date(2019, 1, 1);

            _allDays = Enumerable
                .Range(1, 365)
                .Select(day => Summary.DailySummary(startDate.AddDays(day - 1), $"What I did on day {day}"));

            _allWeeks = Enumerable
                .Range(1, 52)
                .Select(week => Summary.WeeklySummary(startDate.AddDays(7 * (week - 1)), $"What I did in week {week}"));

            _allMonths = Enumerable
                .Range(1, 12)
                .Select(month => Summary.MonthlySummary(((Month)startDate).AddMonths(month - 1), $"What I did in month {month}"));

        }

        public static IEnumerable<object[]> InRange_DayRanges_Inputs()
        {
            yield return new object[] { new Date(2019, 1, 1).SingleDayRange(), 1 };
            yield return new object[] { new Date(2019, 1, 1).RangeFromDays(10), 10 };
            yield return new object[] { new Date(2019, 12, 31).SingleDayRange(), 1 };
            yield return new object[] { new Date(2019, 12, 31).RangeFromDays(10), 1 };
            yield return new object[] { new Date(2020, 1, 1).RangeFromDays(10), 0 };
        }

        [Theory]
        [MemberData(nameof(InRange_DayRanges_Inputs))]
        public void InRange_DayRanges(DateRange dateRange, int expectedDays)
        {
            Assert.Equal(expectedDays, _allDays.InRange(dateRange).Count());
        }

        public static IEnumerable<object[]> InRange_MonthRanges_Inputs()
        {
            yield return new object[] { new Date(2019, 1, 1).SingleDayRange(), 0 };
            yield return new object[] { new Date(2019, 1, 1).RangeFromDays(30), 0 };
            yield return new object[] { new Date(2019, 1, 1).RangeFromDays(31), 1 };
            yield return new object[] { new Date(2019, 1, 1).RangeFromDays(32), 2 };
            yield return new object[] { new Date(2019, 1, 1).RangeFromDays(365), 12 };
            yield return new object[] { new Month(2019, 1).RangeFromMonths(12), 12 };
            yield return new object[] { new Month(2019, 12).RangeFromMonths(1), 1 };
            yield return new object[] { new Month(2019, 12).RangeFromMonths(2), 1 };
        }

        [Theory]
        [MemberData(nameof(InRange_MonthRanges_Inputs))]
        public void InRange_MonthRanges(DateRange dateRange, int expectedDays)
        {
            Assert.Equal(expectedDays, _allMonths.InRange(dateRange).Count());
        }
    }
}
