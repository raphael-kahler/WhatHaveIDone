using System;
using System.Collections.Generic;
using System.Linq;
using Whid.Domain;
using Whid.Domain.Dates;
using Whid.Functional;

namespace Whid.Framework
{
    public class InMemorySummaryService : ISummaryService
    {
        private static readonly Dictionary<Guid, Summary> _store = new Dictionary<Guid, Summary>();

        static InMemorySummaryService()
        {
            var startDate = new Date(2019, 1, 1);

            Enumerable
                .Range(1, 365)
                .Select(day => Summary.DailySummary(startDate.AddDays(day - 1), $"What I did on day {day}"))
                .ForEach(summary => _store.Add(summary.Id, summary));

            Enumerable
                .Range(1, 52)
                .Select(week => Summary.WeeklySummary(startDate.AddDays(7 * (week - 1)), $"What I did in week {week}"))
                .ForEach(summary => _store.Add(summary.Id, summary));

            Enumerable
                .Range(1, 12)
                .Select(month => Summary.MonthlySummary(((Month)startDate).AddMonths(month - 1), $"What I did in month {month}"))
                .ForEach(summary => _store.Add(summary.Id, summary));
        }

        public bool DeleteSummary(Guid id)
        {
            return _store.Remove(id);
        }

        public IEnumerable<Summary> GetSummaries()
        {
            return _store.Values;
        }

        public Option<Summary> GetSummary(Guid id)
        {
            if (_store.TryGetValue(id, out var summary))
            {
                return summary;
            }
            return None.Value;
        }

        public Summary SaveSummary(Summary summary)
        {
            _store[summary.Id] = summary;
            return summary;
        }
    }
}
