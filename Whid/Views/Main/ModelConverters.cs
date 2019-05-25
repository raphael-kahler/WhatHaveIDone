using System;
using Whid.Domain;
using Whid.Framework;

namespace Whid.Views.Main
{
    internal static class ModelConverters
    {
        public static SummaryModel ToViewModel(this Summary summary, ISummaryService service, Action<SummaryModel> removeSummary)
        {
            return new SummaryModel(service, removeSummary)
            {
                Id = summary.Id,
                Content = summary.Content,
                Period = summary.Period
            };
        }

        public static Summary ToDomainModel(this SummaryModel summaryModel)
        {
            return new Summary(summaryModel.Id, summaryModel.Period, summaryModel.Content);
        }
    }
}
