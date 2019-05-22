using Whid.Domain;
using Whid.Framework;

namespace Whid
{
    internal static class ModelConverters
    {
        public static SummaryModel ToViewModel(this Summary summary, ISummaryService service)
        {
            return new SummaryModel(service)
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
