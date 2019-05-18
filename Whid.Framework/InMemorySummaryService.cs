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
        private static Dictionary<Guid, Summary> _store = new Dictionary<Guid, Summary>();

        static InMemorySummaryService()
        {
            Enumerable
                .Range(1, 31)
                .Select(day => new Summary(new Date(2019, 1, day).SingleDayRange(), $"What I did on day {day}"))
                .ForEach(summary => _store.Add(summary.Id, summary));

            Enumerable
                .Range(1, 5)
                .Select(week => new Summary(new Date(2019, 1, 1 + 7 * (week - 1)).RangeFromWeeks(1), $"What I did in week {week}"))
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
