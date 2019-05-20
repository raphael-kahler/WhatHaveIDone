﻿using System.Collections.Generic;
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
        private PeriodOrdering _periodOrdering;
        private ISummaryService _service;

        private PeriodType mainSummaryType;
        public PeriodType MainSummaryType
        {
            get => mainSummaryType;
            set => SetProperty(ref mainSummaryType, value);
        }

        private PeriodType nextSummaryType;
        public PeriodType NextSummaryType
        {
            get => nextSummaryType;
            set => SetProperty(ref nextSummaryType, value);
        }

        private ObservableCollection<SummaryModel> summaries;
        public ObservableCollection<SummaryModel> Summaries
        {
            get => summaries;
            set => SetProperty(ref summaries, value);
        }

        private ObservableCollection<SummaryModel> summarizedSummaries;
        public ObservableCollection<SummaryModel> SummarizedSummaries
        {
            get => summarizedSummaries;
            set => SetProperty(ref summarizedSummaries, value);
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
                    SummarizedSummaries
                        .ForEach(s => s.Highlighted = selectedSummary.Summary.Summarizes(s.Summary));

                    FirstHighlightedSummary = SummarizedSummaries.First(s => selectedSummary.Summary.Summarizes(s.Summary));
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
            _periodOrdering = new PeriodOrdering(new List<PeriodTypeEnum> { PeriodTypeEnum.Day, PeriodTypeEnum.Week, PeriodTypeEnum.Month });

            ShowSmallerSummariesCommand = new RelayCommand(ShowSmallerSummaries, () => _periodOrdering.SummarizesOthers(NextSummaryType.Type));
            ShowBiggerSummariesCommand = new RelayCommand(ShowBiggerSummaries, () => _periodOrdering.SummarizedByOthers(MainSummaryType.Type));

            ShowSummaries(PeriodTypeEnum.Month);
        }

        private void ShowSummaries(PeriodTypeEnum type)
        {
            MainSummaryType = PeriodType.FromTypeEnum(type);
            NextSummaryType = PeriodType.FromTypeEnum(_periodOrdering.GetSummarizedType(type));

            BiggerSummaryTypeVisibility = ShowBiggerSummariesCommand.CanExecute(null) ? Visibility.Visible : Visibility.Hidden;
            SmallerSummaryTypeVisibility = ShowSmallerSummariesCommand.CanExecute(null) ? Visibility.Visible : Visibility.Hidden;

            var allSummaries = _service.GetSummaries().OrderBy(s => s.Period.DateRange.StartTime);
            Summaries = new ObservableCollection<SummaryModel>(allSummaries.OfSummaryType(MainSummaryType).Select(s => new SummaryModel { Summary = s }));
            SummarizedSummaries = new ObservableCollection<SummaryModel>(allSummaries.OfSummaryType(NextSummaryType).Select(s => new SummaryModel { Summary = s }));
        }

        public RelayCommand ShowSmallerSummariesCommand { get; }
        private void ShowSmallerSummaries()
        {
            ShowSummaries(NextSummaryType.Type);
            ShowBiggerSummariesCommand.RaiseCanExecuteChanged();
            ShowSmallerSummariesCommand.RaiseCanExecuteChanged();
        }

        public RelayCommand ShowBiggerSummariesCommand { get; }
        private void ShowBiggerSummaries()
        {
            ShowSummaries(_periodOrdering.GetSummarizingType(MainSummaryType.Type));
            ShowBiggerSummariesCommand.RaiseCanExecuteChanged();
            ShowSmallerSummariesCommand.RaiseCanExecuteChanged();
        }
    }
}