using System.Collections.Generic;
using System.Linq;
using Whid.Domain.Dates;

namespace Whid.Domain
{
    public static class SummaryExtensions
    {
        /// <summary>
        /// Check if the current summary summarizes a specified other summary.
        /// </summary>
        /// <param name="summary">The current summary.</param>
        /// <param name="other">The specified other summary.</param>
        /// <returns>True if the current summary summarizes the specified other summary, false otherwise.</returns>
        public static bool Summarizes(this Summary summary, Summary other) =>
            summary.Period.Type.EncompassesType(other.Period.Type) && summary.Period.DateRange.PartiallyIncludesDateRange(other.Period.DateRange);

        /// <summary>
        /// Filter a collection of summaries to include only those whose dates are at least partially included in the specified date range.
        /// </summary>
        /// <param name="summaries">The collection of summaries.</param>
        /// <param name="dateRange">The specified date range.</param>
        /// <returns>All summaries that are at least partially included in the date range.</returns>
        public static IEnumerable<Summary> InRange(this IEnumerable<Summary> summaries, DateRange dateRange) =>
            summaries.Where(summary => dateRange.PartiallyIncludesDateRange(summary.Period.DateRange));

        /// <summary>
        /// Filter a collection of summaries to include only those which are of the specified summary type.
        /// </summary>
        /// <param name="summaries">The collection of summaries.</param>
        /// <param name="summaryType">The summary type of summaries to include in the result.</param>
        /// <returns>All summaries that are of the specified type.</returns>
        public static IEnumerable<Summary> OfSummaryType(this IEnumerable<Summary> summaries, PeriodType summaryType) =>
            summaries.Where(summary => summary.Period.Type.Equals(summaryType));

        /// <summary>
        /// Filter a collection of summaries to include only those which are summarized by the specified summary type.
        /// </summary>
        /// <param name="summaries">The collection of summaries.</param>
        /// <param name="summaryType">The summary type that should summarize the summaries.</param>
        /// <returns>All summaries of the type which is summarized by the specified summary type.</returns>
        public static IEnumerable<Summary> SummarizedBy(this IEnumerable<Summary> summaries, PeriodType summaryType) =>
            summaries.Where(summary => summaryType.EncompassesType(summary.Period.Type));

        /// <summary>
        /// Filter a collection of summaries to include only those which are summarized by the specified summary.
        /// This means that the selected summaries are included in the date range of the specified summary and are summarized by the type of the specified summary.
        /// </summary>
        /// <param name="summaries">The collection of summaries.</param>
        /// <param name="summaryType">The summary that should summarize the summaries.</param>
        /// <returns>All summaries of the type which is summarized by the specified summary.</returns>
        public static IEnumerable<Summary> SummarizedBy(this IEnumerable<Summary> summaries, Summary summary) =>
            summaries.SummarizedBy(summary.Period.Type).InRange(summary.Period.DateRange);
    }
}
