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

        [Fact]
        public void OfType_DailyType()
        {
            Assert.Equal(365, _allDays.OfSummaryType(SummaryType.DailySummary).Count());
            Assert.Empty(_allWeeks.OfSummaryType(SummaryType.DailySummary));
            Assert.Empty(_allMonths.OfSummaryType(SummaryType.DailySummary));
        }

        [Fact]
        public void OfType_WeeklyType()
        {
            Assert.Empty(_allDays.OfSummaryType(SummaryType.WeeklySummary));
            Assert.Equal(52, _allWeeks.OfSummaryType(SummaryType.WeeklySummary).Count());
            Assert.Empty(_allMonths.OfSummaryType(SummaryType.WeeklySummary));
        }

        [Fact]
        public void OfType_MonthlyType()
        {
            Assert.Empty(_allDays.OfSummaryType(SummaryType.MonthlySummary));
            Assert.Empty(_allWeeks.OfSummaryType(SummaryType.MonthlySummary));
            Assert.Equal(12, _allMonths.OfSummaryType(SummaryType.MonthlySummary).Count());
        }

        [Fact]
        public void SummarizedBy_DailySummary()
        {
            Assert.Empty(_allDays.SummarizedBy(SummaryType.DailySummary));
            Assert.Empty(_allWeeks.SummarizedBy(SummaryType.DailySummary));
            Assert.Empty(_allMonths.SummarizedBy(SummaryType.DailySummary));
        }

        [Fact]
        public void SummarizedBy_WeeklySummary()
        {
            Assert.Equal(365, _allDays.SummarizedBy(SummaryType.WeeklySummary).Count());
            Assert.Empty(_allWeeks.SummarizedBy(SummaryType.WeeklySummary));
            Assert.Empty(_allMonths.SummarizedBy(SummaryType.WeeklySummary));
        }

        [Fact]
        public void SummarizedBy_MonthlySummary()
        {
            Assert.Empty(_allDays.SummarizedBy(SummaryType.MonthlySummary));
            Assert.Equal(52, _allWeeks.SummarizedBy(SummaryType.MonthlySummary).Count());
            Assert.Empty(_allMonths.SummarizedBy(SummaryType.MonthlySummary));
        }
    }
}
