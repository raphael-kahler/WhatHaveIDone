using System;
using System.Collections.Generic;
using Whid.Domain.Dates;

namespace Whid.Domain
{
    public class SummaryPeriod : IEquatable<SummaryPeriod>
    {
        public DateRange DateRange { get; }
        public PeriodType Type { get; }
        public string FormatString => Type.FormatDateRange(DateRange);

        public SummaryPeriod(PeriodType type, DateRange dateRange)
        {
            DateRange = dateRange ?? throw new ArgumentNullException(nameof(dateRange));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }


        #region Equality and Hashcode methods
        public override bool Equals(object obj) => Equals(obj as SummaryPeriod);

        public bool Equals(SummaryPeriod other) =>
            other != null &&
            EqualityComparer<DateRange>.Default.Equals(DateRange, other.DateRange) &&
            EqualityComparer<PeriodType>.Default.Equals(Type, other.Type);

        public override int GetHashCode()
        {
            var hashCode = -335260285;
            hashCode = hashCode * -1521134295 + EqualityComparer<DateRange>.Default.GetHashCode(DateRange);
            hashCode = hashCode * -1521134295 + EqualityComparer<PeriodType>.Default.GetHashCode(Type);
            return hashCode;
        }

        public static bool operator ==(SummaryPeriod left, SummaryPeriod right) =>
            EqualityComparer<SummaryPeriod>.Default.Equals(left, right);

        public static bool operator !=(SummaryPeriod left, SummaryPeriod right) =>
            !(left == right);
        #endregion
    }
}
