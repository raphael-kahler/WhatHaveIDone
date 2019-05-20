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
        /// The name of the summary.
        /// </summary>
        public string Name { get; }

        public PeriodTypeEnum Type { get; }

        private Func<DateRange, string> DateFormat { get; }

        public PeriodType Encompasses { get; private set; }
        public PeriodType EncompassedBy { get; private set; }

        public bool EncompassesOthers => null != Encompasses;
        public bool IsEncompassedByOthers => null != EncompassedBy;

        private void SetToEncompass(PeriodType other)
        {
            Encompasses = other;
            if (null != other)
            {
                other.EncompassedBy = this;
            }
        }

        static PeriodType()
        {
            // Define ordering of period types.
            WeeklySummary.SetToEncompass(DailySummary);
            MonthlySummary.SetToEncompass(WeeklySummary);
        }

        private PeriodType(string name, PeriodTypeEnum type, Func<DateRange, string> dateFormat)
        {
            Type = type;
            Name = name;
            DateFormat = dateFormat;
        }

        private static PeriodType DailySummary { get; } = 
            new PeriodType("Daily Summary", PeriodTypeEnum.Day, dateRange => dateRange.StartTime.ToString("d MMMM yyyy"));
        private static PeriodType WeeklySummary { get; } = 
            new PeriodType("Weekly Summary", PeriodTypeEnum.Week, dateRange => dateRange.StartTime.ToString("d MMMM yyyy") + " - " + dateRange.EndTime.ToString("d MMMM yyyy"));
        private static PeriodType MonthlySummary { get; } = 
            new PeriodType("Monthly Summary", PeriodTypeEnum.Month, dateRange => dateRange.StartTime.ToString("MMMM yyyy"));

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

        public string FormatDateRange(DateRange dateRange) => DateFormat(dateRange);

        /// <summary>
        /// Check if the summary type directly summarizes the specified other summary type.
        /// </summary>
        /// <param name="other">The other summary type.</param>
        /// <returns>True if the summary type directly summarizes the specified summary type. False otherwise.</returns>
        public bool EncompassesType(PeriodType other) =>
            other == Encompasses;

        /// <summary>
        /// Check if the summary type summarizes the specified other type, either directly or indirectly.
        /// This check evaluates to true if the give type summarizes zero to many other types which in turn summarize the specified type.
        /// </summary>
        /// <param name="other">The other summary type.</param>
        /// <returns>True if the summary type summarizes the specified summary type. False otherwise.</returns>
        public bool EncompassesTypeRecursively(PeriodType other) =>
            null == Encompasses ? false :
            other == Encompasses ? true :
            Encompasses.EncompassesTypeRecursively(other);


        #region Equality and Hashcode methods
        public override bool Equals(object obj) => Equals(obj as PeriodType);
        public bool Equals(PeriodType other) => other != null && Name == other.Name;
        public static bool operator ==(PeriodType left, PeriodType right) => EqualityComparer<PeriodType>.Default.Equals(left, right);
        public static bool operator !=(PeriodType left, PeriodType right) => !(left == right);
        public override int GetHashCode() => Name.GetHashCode();
        #endregion
    }
}