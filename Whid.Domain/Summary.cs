using System;
using Whid.Domain.Dates;

namespace Whid.Domain
{
    public class Summary
    {
        public Guid Id { get; }
        public SummaryPeriod Period { get; }
        public string Content { get; }

        public Summary(Guid id, SummaryPeriod period, string content)
        {
            Id = id;
            Period = period ?? throw new ArgumentNullException(nameof(period));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public static Summary DailySummary(Date date, string content) =>
            new Summary(Guid.Empty, new SummaryPeriod(PeriodType.FromTypeEnum(PeriodTypeEnum.Day), date.SingleDayRange()), content);

        public static Summary WeeklySummary(Date startDate, string content) =>
            new Summary(Guid.Empty, new SummaryPeriod(PeriodType.FromTypeEnum(PeriodTypeEnum.Week), startDate.RangeFromWeeks(1)), content);

        public static Summary MonthlySummary(Month month, string content) =>
            new Summary(Guid.Empty, new SummaryPeriod(PeriodType.FromTypeEnum(PeriodTypeEnum.Month), month.SingleMonthRange()), content);
    }
}
