namespace Whid.Domain.Dates
{
    public static class MonthExtensions
    {
        /// <summary>
        /// Create a DateRange with the specified number of months starting from the current Month.
        /// A range with numMOnths == 1 will end on the same month that it starts.
        /// A range with numMOnths == 2 will end one month after it starts.
        /// </summary>
        /// <param name="month">The month to use as the start date.</param>
        /// <param name="nunMonths">The number of months in the DateRange.</param>
        /// <returns>The created DateRange.</returns>
        public static DateRange RangeFromMonths(this Month month, int nunMonths) =>
            new DateRange(month, month.AddMonths(nunMonths).ToDate().AddDays(-1));

        /// <summary>
        /// Create a DateRange that is one month long.
        /// </summary>
        /// <param name="month">The month of the range.</param>
        /// <returns>The created DateRange.</returns>
        public static DateRange SingleMonthRange(this Month month) =>
            month.RangeFromMonths(1);
    }
}
