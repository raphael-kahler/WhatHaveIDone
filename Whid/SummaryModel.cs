using Whid.Domain;
using Whid.ViewModels;

namespace Whid
{
    public class SummaryModel : BaseViewModel
    {
        private Summary summary;
        public Summary Summary
        {
            get => summary;
            set => SetProperty(ref summary, value);
        }

        private bool highlighted;
        public bool Highlighted
        {
            get => highlighted;
            set => SetProperty(ref highlighted, value);
        }
    }
}
