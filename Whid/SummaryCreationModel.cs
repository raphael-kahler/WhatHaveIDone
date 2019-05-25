using System;
using Whid.Domain;
using Whid.Helpers;

namespace Whid
{
    public class SummaryCreationModel : BaseViewModel
    {
        private PeriodType periodType;
        public PeriodType PeriodType
        {
            get => periodType;
            set => SetProperty(ref periodType, value);
        }

        private DateTime periodTime;
        public DateTime PeriodTime
        {
            get => periodTime;
            set => SetProperty(ref periodTime, value);
        }

        public Summary ConvertToSummary() =>
            Summary.FromPeriodType(PeriodType, PeriodTime, string.Empty);
    }
}
