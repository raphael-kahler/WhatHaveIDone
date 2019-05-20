using System;
using System.Collections.Generic;
using Whid.Domain.Dates;

namespace Whid.Domain
{
    /// <summary>
    /// Class that describes the type of a summary period.
    /// Period types are ordered so that summaries of one type are summarized by the subsequent type.
    /// </summary>
    public sealed class PeriodType : IEquatable<PeriodType>
    {
        /// <summary>
        /// The ordering of the summary.A type summarizes any type that has a lower ordering number.
        /// </summary>
        private int OrderingNumber { get; }

        /// <summary>
        /// The name of the summary.
        /// </summary>
        public string Name { get; }

        public PeriodTypeEnum Type { get; }

        private Func<DateRange, string> DateFormat { get; }

        private PeriodType(PeriodTypeEnum type, int orderingNumger, string name, Func<DateRange, string> dateFormat)
        {
            Type = type;
            Name = name;
            DateFormat = dateFormat;
            OrderingNumber = orderingNumger;
        }

        public static PeriodType FromTypeEnum(PeriodTypeEnum typeEnum)
        {
            switch (typeEnum)
            {
                case PeriodTypeEnum.Day: return DailySummary;
                case PeriodTypeEnum.Week: return WeeklySummary;
                case PeriodTypeEnum.Month: return MonthlySummary;
                default: throw new ArgumentOutOfRangeException(nameof(typeEnum), typeEnum, "Invalid period type enum value");
            }
        }

        public static PeriodType DailySummary { get; } = new PeriodType(PeriodTypeEnum.Day, 1, "Daily Summary", dateRange => dateRange.StartTime.ToString("d MMMM yyyy"));
        public static PeriodType WeeklySummary { get; } = new PeriodType(PeriodTypeEnum.Week, 2, "Weekly Summary", dateRange => dateRange.StartTime.ToString("d MMMM yyyy") + " - " + dateRange.EndTime.ToString("d MMMM yyyy"));
        public static PeriodType MonthlySummary { get; } = new PeriodType(PeriodTypeEnum.Month, 3, "Monthly Summary", dateRange => dateRange.StartTime.ToString("MMMM yyyy"));

        public string FormatDateRange(DateRange dateRange) => DateFormat(dateRange);


        /// <summary>
        /// Check if the summary type directly summarizes the specified other summary type.
        /// </summary>
        /// <param name="other">The other summary type.</param>
        /// <returns>True if the summary type directly summarizes the specified summary type. False otherwise.</returns>
        public bool Summarizes(PeriodType other) =>
            OrderingNumber - 1 == other.OrderingNumber;

        /// <summary>
        /// Check if the summary type summarizes the specified other type, either directly or indirectly.
        /// This check evaluates to true if the give type summarizes zero to many other types which in turn summarize the specified type.
        /// </summary>
        /// <param name="other">The other summary type.</param>
        /// <returns>True if the summary type summarizes the specified summary type. False otherwise.</returns>
        public bool SummarizesRecursively(PeriodType other) =>
            OrderingNumber > other.OrderingNumber;


        #region Equality and Hashcode methods
        public override bool Equals(object obj) => Equals(obj as PeriodType);
        public bool Equals(PeriodType other) => other != null && Name == other.Name;
        public static bool operator ==(PeriodType left, PeriodType right) => EqualityComparer<PeriodType>.Default.Equals(left, right);
        public static bool operator !=(PeriodType left, PeriodType right) => !(left == right);
        public override int GetHashCode() => Name.GetHashCode();
        #endregion
    }

    //public sealed class DailySummary : PeriodType
    //{
    //    protected override int OrderingNumber => 1;
    //    public override string Name => "Daily summary";
    //    public override string FormatDateRange(DateRange dateRange) => dateRange.StartTime.ToString("d");
    //}

    //public sealed class WeeklySummary : PeriodType
    //{
    //    protected override int OrderingNumber => 2;
    //    public override string Name => "Weekly summary";
    //    public override string FormatDateRange(DateRange dateRange) => dateRange.StartTime.ToString("d");
    //}

    //public sealed class MonthlySummary : PeriodType
    //{
    //    protected override int OrderingNumber => 3;
    //    public override string Name => "Monthly summary";
    //    public override string FormatDateRange(DateRange dateRange) => dateRange.StartTime.ToString("d");
    //}
}