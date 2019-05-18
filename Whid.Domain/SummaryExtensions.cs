using System.Collections.Generic;
using System.Linq;
using Whid.Domain.Dates;

namespace Whid.Domain
{
    public static class SummaryExtensions
    {
        /// <summary>
        /// Filter a collection of summaries to include only those whose dates are at least partially included in the specified date range.
        /// </summary>
        /// <param name="summaries">The collection of summaries.</param>
        /// <param name="dateRange">The specified date range.</param>
        /// <returns>All summaries that are at least partially included in the date range.</returns>
        public static IEnumerable<Summary> InRange(this IEnumerable<Summary> summaries, DateRange dateRange) =>
            summaries.Where(summary => dateRange.PartiallyIncludesDateRange(summary.Period));
    }
}
