using System.Collections.Generic;
using Whid.Domain;
using Whid.Framework;

namespace Whid
{
    internal class MainWindowViewModel
    {
        public IEnumerable<Summary> Summaries { get; set; }

        public MainWindowViewModel(ISummaryService service)
        {
            Summaries = service.GetSummaries();
        }
    }
}