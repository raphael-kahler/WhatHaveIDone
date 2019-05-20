using Whid.Domain;
using Whid.Domain.Dates;

namespace Whid.Framework.DB
{
    internal static class ModelConverter
    {
        public static SummaryModel ToDbModel(this Summary summary)
        {
            return new SummaryModel
            {
                Id = summary.Id,
                StartTime = summary.Period.DateRange.StartTime,
                EndTime = summary.Period.DateRange.EndTime,
                PeriodType = (int)summary.Period.Type.Type,
                Content = summary.Content,
            };
        }

        public static Summary ToDomainModel(this SummaryModel summary)
        {
            return new Summary
            (
                id: summary.Id,
                period: new SummaryPeriod
                (
                    type: PeriodType.FromTypeEnum((PeriodTypeEnum)summary.PeriodType),
                    dateRange: new DateRange((Date)summary.StartTime, (Date)summary.EndTime)
                ),
                content: summary.Content
            );
        }
    }
}
