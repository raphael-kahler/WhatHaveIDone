using System;
using System.Collections.Generic;

namespace Whid.Domain
{
    /// <summary>
    /// A summary type. Summary types are ordered so that each type is summarized by the subsequent type.
    /// </summary>
    public sealed class SummaryType : IEquatable<SummaryType>
    {
        /// <summary>
        /// The name of the summary.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The ordering of the summary. A type summarizes any type that has a lower ordering number.
        /// </summary>
        private int OrderingNumber { get; }

        private SummaryType(string name, int orderingNumber)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            OrderingNumber = orderingNumber;
        }

        public static SummaryType DailySummary { get; } = new SummaryType("Daily summary", 1);
        public static SummaryType WeeklySummary { get; } = new SummaryType("Weekly summary", 2);
        public static SummaryType MonthlySummary { get; } = new SummaryType("Monthly summary", 3);

        /// <summary>
        /// Check if the summary type directly summarizes the specified other summary type.
        /// </summary>
        /// <param name="other">The other summary type.</param>
        /// <returns>True if the summary type directly summarizes the specified summary type. False otherwise.</returns>
        public bool Summarizes(SummaryType other) =>
            OrderingNumber - 1 == other.OrderingNumber;

        /// <summary>
        /// Check if the summary type summarizes the specified other type, either directly or indirectly.
        /// This check evaluates to true if the give type summarizes zero to many other types which in turn summarize the specified type.
        /// </summary>
        /// <param name="other">The other summary type.</param>
        /// <returns>True if the summary type summarizes the specified summary type. False otherwise.</returns>
        public bool SummarizesRecursively(SummaryType other) =>
            OrderingNumber > other.OrderingNumber;

        #region Equality and Hashcode methods
        public override bool Equals(object obj) => Equals(obj as SummaryType);
        public bool Equals(SummaryType other) => other != null && OrderingNumber == other.OrderingNumber;
        public static bool operator ==(SummaryType left, SummaryType right) => EqualityComparer<SummaryType>.Default.Equals(left, right);
        public static bool operator !=(SummaryType left, SummaryType right) => !(left == right);
        public override int GetHashCode() => OrderingNumber.GetHashCode();
        #endregion
    }
}