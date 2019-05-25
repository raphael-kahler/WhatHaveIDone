using System.Collections.Generic;
using System.Linq;
using Whid.Functional;

namespace Whid
{
    internal class HighlightedSummaryList
    {
        private List<SummaryModel> _summaries = new List<SummaryModel>();

        public void ReplaceSummariesWith(SummaryModel newSummary) =>
            ReplaceSummariesWith(new List<SummaryModel> { newSummary });

        public void ReplaceSummariesWith(IEnumerable<SummaryModel> newSummaries)
        {
            HighlightSummaries(false);
            _summaries = newSummaries.ToList();
            HighlightSummaries(true);
        }

        public void HighlightSummaries(bool highlighted) =>
            _summaries.ForEach(s => s.Highlighted = highlighted);

        public Option<SummaryModel> FirstSummary() =>
            _summaries.FirstOrDefault();
    }
}
