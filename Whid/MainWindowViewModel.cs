using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Whid.Domain;
using Whid.Framework;
using Whid.Functional;
using Whid.Helpers;

namespace Whid
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private ISummaryService _service;

        private PeriodType mainSummaryType;
        public PeriodType MainSummaryType
        {
            get => mainSummaryType;
            set => SetProperty(ref mainSummaryType, value);
        }

        private ObservableCollection<SummaryModel> summaries;
        public ObservableCollection<SummaryModel> Summaries
        {
            get => summaries;
            set => SetProperty(ref summaries, value);
        }

        private ObservableCollection<SummaryModel> encompassedSummaries;
        public ObservableCollection<SummaryModel> EncompassedSummaries
        {
            get => encompassedSummaries;
            set => SetProperty(ref encompassedSummaries, value);
        }

        private SummaryModel selectedSummary;
        public SummaryModel SelectedSummary
        {
            get => selectedSummary;
            set
            {
                bool wasSet = SetProperty(ref selectedSummary, value);
                if (wasSet && null != selectedSummary)
                {
                    EncompassedSummaries
                        .ForEach(s => s.Highlighted = selectedSummary.Summary.Summarizes(s.Summary));

                    FirstHighlightedSummary = EncompassedSummaries.First(s => selectedSummary.Summary.Summarizes(s.Summary));
                }
            }
        }

        private SummaryModel firstHighlightedSummary;
        public SummaryModel FirstHighlightedSummary
        {
            get => firstHighlightedSummary;
            set => SetProperty(ref firstHighlightedSummary, value);
        }

        private Visibility biggerSummaryTypeVisibility;
        public Visibility BiggerSummaryTypeVisibility
        {
            get => biggerSummaryTypeVisibility;
            set => SetProperty(ref biggerSummaryTypeVisibility, value);
        }

        private Visibility smallerSummaryTypeVisibility;
        public Visibility SmallerSummaryTypeVisibility
        {
            get => smallerSummaryTypeVisibility;
            set => SetProperty(ref smallerSummaryTypeVisibility, value);
        }

        public MainWindowViewModel(ISummaryService service)
        {
            _service = service;

            ShowSmallerSummariesCommand = new RelayCommand(ShowSmallerSummaries, () => MainSummaryType.Encompasses.EncompassesOthers);
            ShowBiggerSummariesCommand = new RelayCommand(ShowBiggerSummaries, () => MainSummaryType.IsEncompassedByOthers);

            ShowSummaries(PeriodTypeEnum.Month);
        }

        private void ShowSummaries(PeriodTypeEnum type)
        {
            MainSummaryType = PeriodType.FromTypeEnum(type);

            BiggerSummaryTypeVisibility = ShowBiggerSummariesCommand.CanExecute(null) ? Visibility.Visible : Visibility.Hidden;
            SmallerSummaryTypeVisibility = ShowSmallerSummariesCommand.CanExecute(null) ? Visibility.Visible : Visibility.Hidden;

            var allSummaries = _service.GetSummaries().OrderBy(s => s.Period.DateRange.StartTime);
            Summaries = new ObservableCollection<SummaryModel>(allSummaries.OfSummaryType(MainSummaryType).Select(s => new SummaryModel { Summary = s }));
            EncompassedSummaries = new ObservableCollection<SummaryModel>(allSummaries.OfSummaryType(MainSummaryType.Encompasses).Select(s => new SummaryModel { Summary = s }));
        }

        public RelayCommand ShowSmallerSummariesCommand { get; }
        private void ShowSmallerSummaries()
        {
            ShowSummaries(MainSummaryType.Encompasses.Type);
            ShowBiggerSummariesCommand.RaiseCanExecuteChanged();
            ShowSmallerSummariesCommand.RaiseCanExecuteChanged();
        }

        public RelayCommand ShowBiggerSummariesCommand { get; }
        private void ShowBiggerSummaries()
        {
            ShowSummaries(MainSummaryType.EncompassedBy.Type);
            ShowBiggerSummariesCommand.RaiseCanExecuteChanged();
            ShowSmallerSummariesCommand.RaiseCanExecuteChanged();
        }
    }
}