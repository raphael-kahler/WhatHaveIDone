using System;
using System.Collections.Generic;

namespace Whid.Domain.Dates
{
    public sealed class Month : IEquatable<Month>
    {
        private DateTime Value { get; }
        public Month(int year, int month) =>
            Value = new DateTime(year, month, 1);

        public Month AddMonths(int monthsToAdd) =>
            (Month)Value.AddMonths(monthsToAdd);

        public Date ToDate() => this;

        public static implicit operator DateTime(Month month) => month.Value;
        public static implicit operator Date(Month month) => (Date)month.Value;
        public static explicit operator Month(DateTime date) => new Month(date.Year, date.Month);

        public static bool operator ==(Month left, Month right) => EqualityComparer<Month>.Default.Equals(left, right);
        public static bool operator !=(Month left, Month right) => !(left == right);
        public override bool Equals(object obj) => Equals(obj as Month);
        public bool Equals(Month other) => other != null && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}
