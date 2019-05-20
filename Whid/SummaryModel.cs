using System.ComponentModel;
using Whid.Domain;

namespace Whid
{
    public class SummaryModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private Summary summary;
        public Summary Summary
        {
            get => summary;
            set
            {
                if (value != summary)
                {
                    summary = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Summary)));
                }
            }
        }

        private bool highlighted;
        public bool Highlighted
        {
            get => highlighted;
            set
            {
                if (value != highlighted)
                {
                    highlighted = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Highlighted)));
                }
            }
        }
    }
}
