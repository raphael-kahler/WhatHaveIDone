using System;
using Whid.Domain.Dates;

namespace Whid.Domain
{
    public class Summary
    {
        public Guid Id { get; }
        public IDateRange Period { get; }
        public string Content { get; }

        public Summary(IDateRange period, string content)
        {
            Id = Guid.NewGuid();
            Period = period ?? throw new ArgumentNullException(nameof(period));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

    }
}
