using System;
using System.Collections.Generic;
using System.Text;

namespace Whid.Framework.DB
{
    internal class SummaryModel
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Content { get; set; }

        public int PeriodType { get; set; }
    }
}
