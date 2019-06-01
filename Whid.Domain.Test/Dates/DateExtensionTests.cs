using System.Collections.Generic;
using Whid.Domain.Dates;
using Xunit;

namespace Whid.Domain.Test.Dates
{
    public class DateExtensionTests
    {
        public static IEnumerable<object[]> FirstDayOfWeek_Inputs()
        {
            yield return new object[] { new Date(year: 2019, month: 1, day: 1), new Date(year: 2018, month: 12, day: 30) }; // Tuesday mapped to Sunday
            yield return new object[] { new Date(year: 2018, month: 12, day: 30), new Date(year: 2018, month: 12, day: 30) }; // Sunday mapped to Sunday
            yield return new object[] { new Date(year: 2018, month: 12, day: 29), new Date(year: 2018, month: 12, day: 23) }; // Saturday mapped to Sunday
        }

        [Theory]
        [MemberData(nameof(FirstDayOfWeek_Inputs))]
        public void FirstDayOfWeek(Date day, Date expectedFirstDayOfWeek)
        {
            Assert.Equal(expectedFirstDayOfWeek, day.FirstDayOfWeek());
        }
    }
}
