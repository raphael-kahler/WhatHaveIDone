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
            Content = content;
        }

        public static Summary DailySummary(Date date, string content) =>
            new Summary(Guid.Empty, new SummaryPeriod(PeriodType.FromTypeEnum(PeriodTypeEnum.Day), date.SingleDayRange()), content);

        public static Summary WeeklySummary(Date startDate, string content) =>
            new Summary(Guid.Empty, new SummaryPeriod(PeriodType.FromTypeEnum(PeriodTypeEnum.Week), startDate.FirstDayOfWeek().RangeFromWeeks(1)), content);

        public static Summary MonthlySummary(Month month, string content) =>
            new Summary(Guid.Empty, new SummaryPeriod(PeriodType.FromTypeEnum(PeriodTypeEnum.Month), month.SingleMonthRange()), content);

        public static Summary FromPeriodType(PeriodType periodType, DateTime startDate, string content)
        {
            switch (periodType.Type)
            {
                case PeriodTypeEnum.Day: return DailySummary((Date)startDate, content);
                case PeriodTypeEnum.Week: return WeeklySummary((Date)startDate, content);
                case PeriodTypeEnum.Month: return MonthlySummary((Month)startDate, content);
                default: throw new ArgumentOutOfRangeException(nameof(periodType), periodType, $"Speficied period type \"{periodType}\" is not a valid type for constructing a summary.");
            }
        }
    }
}
