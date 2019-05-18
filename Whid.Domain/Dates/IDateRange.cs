using System;

namespace Whid.Domain.Dates
{
    public interface IDateRange
    {
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        TimeSpan GetDuration();
    }
}