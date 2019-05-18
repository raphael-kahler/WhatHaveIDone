using System;
using System.Collections.Generic;
using Whid.Domain;
using Whid.Functional;

namespace Whid.Framework
{
    public interface ISummaryService
    {
        IEnumerable<Summary> GetSummaries();
        Option<Summary> GetSummary(Guid id);
        Summary SaveSummary(Summary summary);
        bool DeleteSummary(Guid id);
    }
}
