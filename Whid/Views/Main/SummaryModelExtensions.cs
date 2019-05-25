namespace Whid.Views.Main
{
    internal static class SummaryModelExtensions
    {
        public static void HighlightSummary(this SummaryModel summary, bool highlighted)
        {
            if (null != summary)
            {
                summary.Highlighted = highlighted;
            }
        }
    }
}
