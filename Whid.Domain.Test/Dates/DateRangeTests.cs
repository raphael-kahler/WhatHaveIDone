using System;
using System.Collections.Generic;
using Whid.Domain.Dates;
using Xunit;

namespace Whid.Domain.Test.Dates
{
    public class DateRangeTests
    {
        public static IEnumerable<object[]> IncludesDate_DateTime_Inputs()
        {
            yield return new object[] { new DateTime(year: 999, month: 12, day: 31), false };
            yield return new object[] { new DateTime(year: 999, month: 12, day: 31, hour: 23, minute: 59, second: 59), false };
            yield return new object[] { new DateTime(year: 1000, month: 1, day: 1), true };
            yield return new object[] { new DateTime(year: 2000, month: 1, day: 1), true};
            yield return new object[] { new DateTime(year: 2000, month: 1, day: 1, hour: 23, minute:59, second: 59), true};
            yield return new object[] { new DateTime(year: 2000, month: 1, day: 2), false};
        }

        [Theory]
        [MemberData(nameof(IncludesDate_DateTime_Inputs))]
        public void IncludesDate_DateTime_CheckIsCorrect(DateTime date, bool shouldBeIncluded)
        {
            var sut = new DateRange(new Date(year: 1000, month: 1, day: 1), new Date(year: 2000, month: 1, day: 1));
            Assert.Equal(shouldBeIncluded, sut.IncludesDate(date));
        }

        public static IEnumerable<object[]> IncludesDate_SingleDayRange_Inputs()
        {
            yield return new object[] { new DateTime(year: 999, month: 12, day: 31), false };
            yield return new object[] { new DateTime(year: 999, month: 12, day: 31, hour: 23, minute: 59, second: 59), false };
            yield return new object[] { new DateTime(year: 1000, month: 1, day: 1), true };
            yield return new object[] { new DateTime(year: 1000, month: 1, day: 1, hour: 23, minute:59, second: 59), true};
            yield return new object[] { new DateTime(year: 1000, month: 1, day: 2), false};
        }

        [Theory]
        [MemberData(nameof(IncludesDate_SingleDayRange_Inputs))]
        public void IncludesDate_SingleDayRange_CheckIsCorrect(DateTime date, bool shouldBeIncluded)
        {
            var day = new Date(year: 1000, month: 1, day: 1);
            var sut = new DateRange(day, day);
            Assert.Equal(shouldBeIncluded, sut.IncludesDate(date));
        }

        public static IEnumerable<object[]> IncludesDateRange_SingleDayRange_Inputs()
        {
            yield return new object[] { new Date(999, 12, 31).RangeFromDays(1), false };
            yield return new object[] { new Date(1000, 1, 1).RangeFromDays(1), true};
            yield return new object[] { new Date(1000, 1, 1).RangeFromDays(2), false };
            yield return new object[] { new Date(1000, 1, 2).RangeFromDays(1), false };
        }

        [Theory]
        [MemberData(nameof(IncludesDateRange_SingleDayRange_Inputs))]
        public void IncludesDateRange_SingleDayRange_CheckIsCorrect(IDateRange dateRange, bool shouldBeIncluded)
        {
            DateRange sut = new Date(year: 1000, month: 1, day: 1).RangeFromDays(1);
            Assert.Equal(shouldBeIncluded, sut.IncludesDateRange(dateRange));
        }

        public static IEnumerable<object[]> PartiallyIncludesDateRange_SingleDayRange_Inputs()
        {
            yield return new object[] { new Date(999, 12, 31).RangeFromDays(1), false };
            yield return new object[] { new Date(999, 12, 31).RangeFromDays(2), true};
            yield return new object[] { new Date(999, 12, 31).RangeFromDays(3), false};
            yield return new object[] { new Date(1000, 1, 1).RangeFromDays(1), true};
            yield return new object[] { new Date(1000, 1, 1).RangeFromDays(2), true};
            yield return new object[] { new Date(1000, 1, 1).RangeFromDays(3), false};
            yield return new object[] { new Date(1000, 1, 2).RangeFromDays(1), true };
            yield return new object[] { new Date(1000, 1, 2).RangeFromDays(2), true };
            yield return new object[] { new Date(1000, 1, 2).RangeFromDays(3), false };
        }

        [Theory]
        [MemberData(nameof(PartiallyIncludesDateRange_SingleDayRange_Inputs))]
        public void PartiallyIncludesDateRange_SingleDayRange_CheckIsCorrect(IDateRange dateRange, bool shouldBeIncluded)
        {
            DateRange sut = new Date(year: 1000, month: 1, day: 1).RangeFromDays(2);
            Assert.Equal(shouldBeIncluded, sut.PartiallyIncludesDateRange(dateRange));
        }

        public static IEnumerable<object[]> DurationInDays_CreatedFromDates_Inputs()
        {
            yield return new object[] { new DateRange(new Date(1000, 1, 1), new Date(1000, 1, 1)), 1};
            yield return new object[] { new DateRange(new Date(1000, 1, 1), new Date(1000, 1, 2)), 2};
            yield return new object[] { new DateRange(new Date(1000, 1, 1), new Date(1000, 1, 31)), 31};
        }

        [Theory]
        [MemberData(nameof(DurationInDays_CreatedFromDates_Inputs))]
        public void DurationInDays_CreatedFromDates(DateRange dateRange, int expectedDurationInDays)
        {
            Assert.Equal(TimeSpan.FromDays(expectedDurationInDays), dateRange.GetDuration());
        }


        [Theory]
        [InlineData(1)]
        [InlineData(8)]
        [InlineData(123684)]
        public void DurationInDays_CreatedUsingRangeFromDays(int numDays)
        {
            DateRange sut = new Date(1000, 1, 1).RangeFromDays(numDays);
            Assert.Equal(TimeSpan.FromDays(numDays), sut.GetDuration());
        }


        [Theory]
        [InlineData(1, 7)]
        [InlineData(2, 14)]
        [InlineData(100, 700)]
        public void DurationInDays_CreatedUsingRangeFromWeeks(int weeks, int durationInDays)
        {
            DateRange sut = new Date(1000, 1, 1).RangeFromWeeks(weeks);
            Assert.Equal(TimeSpan.FromDays(durationInDays), sut.GetDuration());
        }

        [Theory]
        [InlineData(2020, 1, 31)]
        [InlineData(2020, 2, 29)]
        [InlineData(2021, 2, 28)]
        public void DurationInDays_CreatedUsingSingleMonthRange(int year, int month, int durationInDays)
        {
            DateRange sut = new Month(year, month).SingleMonthRange();
            Assert.Equal(TimeSpan.FromDays(durationInDays), sut.GetDuration());
        }
    }
}
