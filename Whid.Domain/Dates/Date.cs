using System;
using System.Collections.Generic;

namespace Whid.Domain.Dates
{
    public sealed class Date : IEquatable<Date>
    {
        private DateTime Value { get; }
        public Date(int year, int month, int day) =>
            Value = new DateTime(year, month, day);


        public Date AddDays(int daysToAdd) =>
            (Date)Value.AddDays(daysToAdd);


        public static explicit operator Date(DateTime date) => new Date(date.Year, date.Month, date.Day);

        public static implicit operator DateTime(Date date) => date.Value;


        public static bool operator ==(Date left, Date right) => EqualityComparer<Date>.Default.Equals(left, right);
        public static bool operator !=(Date left, Date right) => !(left == right);
        public override bool Equals(object obj) => Equals(obj as Date);
        public bool Equals(Date other) => other != null && Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
    }
}