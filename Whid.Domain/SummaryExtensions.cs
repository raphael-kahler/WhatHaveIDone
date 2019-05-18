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

        /// <summary>
        /// Filter a collection of summaries to include only those which are of the specified summary type.
        /// </summary>
        /// <param name="summaries">The collection of summaries.</param>
        /// <param name="summaryType">The summary type of summaries to include in the result.</param>
        /// <returns>All summaries that are of the specified type.</returns>
        public static IEnumerable<Summary> OfSummaryType(this IEnumerable<Summary> summaries, SummaryType summaryType) =>
            summaries.Where(summary => summary.Type.Equals(summaryType));

        /// <summary>
        /// Filter a collection of summaries to include only those which are summarized by the specified summary type.
        /// </summary>
        /// <param name="summaries">The collection of summaries.</param>
        /// <param name="summaryType">The summary type that should summarize the the summaries.</param>
        /// <returns>All summaries of the type which is summarized by the specified summary type.</returns>
        public static IEnumerable<Summary> SummarizedBy(this IEnumerable<Summary> summaries, SummaryType summaryType) =>
            summaries.Where(summary => summaryType.Summarizes(summary.Type));
    }
}
