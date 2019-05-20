using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Whid.Domain;
using Whid.Framework;
using Whid.Functional;

namespace Whid
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private PeriodOrdering _periodOrdering;
        private ISummaryService _service;

        private PeriodType mainSummaryType;
        public PeriodType MainSummaryType
        {
            get => mainSummaryType;
            set
            {
                if (value != mainSummaryType)
                {
                    mainSummaryType = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(MainSummaryType)));
                }
            }
        }

        private PeriodType nextSummaryType;
        public PeriodType NextSummaryType
        {
            get => nextSummaryType;
            set
            {
                if (value != nextSummaryType)
                {
                    nextSummaryType = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(NextSummaryType)));
                }
            }
        }

        private ObservableCollection<SummaryModel> summaries;
        public ObservableCollection<SummaryModel> Summaries
        {
            get => summaries;
            set
            {
                if (value != summaries)
                {
                    summaries = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Summaries)));
                }
            }
        }

        private ObservableCollection<SummaryModel> summarizedSummaries;

        public ObservableCollection<SummaryModel> SummarizedSummaries
        {
            get => summarizedSummaries;
            set
            {
                if (value != summarizedSummaries)
                {
                    summarizedSummaries = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(SummarizedSummaries)));
                }
            }
        }

        private SummaryModel selectedSummary;
        public SummaryModel SelectedSummary
        {
            get => selectedSummary;
            set
            {
                if (value != selectedSummary)
                {
                    selectedSummary = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedSummary)));
                    if (null != selectedSummary)
                    {
                        SummarizedSummaries
                            .ForEach(s => s.Highlighted = selectedSummary.Summary.Summarizes(s.Summary));
                    }
                }
            }
        }

        private Visibility biggerSummaryTypeVisibility;
        public Visibility BiggerSummaryTypeVisibility
        {
            get => biggerSummaryTypeVisibility;
            set
            {
                if (value != biggerSummaryTypeVisibility)
                {
                    biggerSummaryTypeVisibility = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(BiggerSummaryTypeVisibility)));
                }
            }
        }

        private Visibility smallerSummaryTypeVisibility;
        public Visibility SmallerSummaryTypeVisibility
        {
            get => smallerSummaryTypeVisibility;
            set
            {
                if (value != smallerSummaryTypeVisibility)
                {
                    smallerSummaryTypeVisibility = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(SmallerSummaryTypeVisibility)));
                }
            }
        }

        public MainWindowViewModel(ISummaryService service)
        {
            _service = service;
            _periodOrdering = new PeriodOrdering(new List<PeriodTypeEnum> { PeriodTypeEnum.Day, PeriodTypeEnum.Week, PeriodTypeEnum.Month });

            ShowSummaries(PeriodTypeEnum.Month);
        }

        private void ShowSummaries(PeriodTypeEnum type)
        {
            MainSummaryType = PeriodType.FromTypeEnum(type);
            NextSummaryType = PeriodType.FromTypeEnum(_periodOrdering.GetSummarizedType(type));

            BiggerSummaryTypeVisibility = _periodOrdering.SummarizedByOthers(MainSummaryType.Type) ? Visibility.Visible : Visibility.Hidden;
            SmallerSummaryTypeVisibility = _periodOrdering.SummarizesOthers(NextSummaryType.Type) ? Visibility.Visible : Visibility.Hidden;

            var allSummaries = _service.GetSummaries().OrderBy(s => s.Period.DateRange.StartTime);
            Summaries = new ObservableCollection<SummaryModel>(allSummaries.OfSummaryType(MainSummaryType).Select(s => new SummaryModel { Summary = s }));
            SummarizedSummaries = new ObservableCollection<SummaryModel>(allSummaries.OfSummaryType(NextSummaryType).Select(s => new SummaryModel { Summary = s }));
        }

        internal void ShowSmallerSummaries()
        {
            ShowSummaries(NextSummaryType.Type);
        }

        internal void ShowBiggerSummaries()
        {
            ShowSummaries(_periodOrdering.GetSummarizingType(MainSummaryType.Type));
        }
    }
}