using System.Collections.Generic;
using System.Linq;
using Whid.Domain;
using Whid.Domain.Dates;

namespace Whid.DesignTime
{
    public class DesignSummaries
    {
        public PeriodType MainSummaryType { get; set; } = PeriodType.FromTypeEnum(PeriodTypeEnum.Month);
        public IEnumerable<SummaryModel> Summaries { get; set; }
        public IEnumerable<SummaryModel> EncompassedSummaries { get; set; }

        public List<PeriodType> PeriodTypes { get; set; } = PeriodType.AllPeriodTypes().ToList();

        public DesignSummaries()
        {
            var startDate = new Date(1739, 1, 1);

            var allDays = Enumerable
                .Range(1, 365)
                .Select(day => Summary.DailySummary(startDate.AddDays(day - 1), $"What I did on day {day}"))
                .Select(s => s.ToViewModel(null));

            var allWeeks = Enumerable
                .Range(1, 52)
                .Select(week => Summary.WeeklySummary(startDate.AddDays(7 * (week - 1)), $"What I did in week {week}"))
                .Select(s => s.ToViewModel(null));

            var allMonths = Enumerable
                .Range(1, 12)
                .Select(month => Summary.MonthlySummary(((Month)startDate).AddMonths(month - 1), $"What I did in month {month}"))
                .Select(s => s.ToViewModel(null));

            Summaries = allMonths;
            EncompassedSummaries = allWeeks;

            Summaries.Skip(1).First().Highlighted = true;
        }
    }
}
