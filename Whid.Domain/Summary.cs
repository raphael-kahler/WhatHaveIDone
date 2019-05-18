using System;
using Whid.Domain.Dates;

namespace Whid.Domain
{
    public class Summary
    {
        public Guid Id { get; }
        public IDateRange Period { get; }
        public string Content { get; }

        public string Type { get; }

        private Summary(IDateRange period, string type, string content)
        {
            Id = Guid.NewGuid();
            Period = period ?? throw new ArgumentNullException(nameof(period));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public static Summary DailySummary(Date date, string content) =>
            new Summary(date.SingleDayRange(), "Daily summary", content);

        public static Summary WeeklySummary(Date startDate, string content) =>
            new Summary(startDate.RangeFromWeeks(1), "Weekly summary", content);

        public static Summary MonthlySummary(Month month, string content) =>
            new Summary(month.SingleMonthRange(), "Monthly summary", content);
    }
}
