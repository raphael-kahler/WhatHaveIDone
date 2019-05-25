using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Whid.Domain;
using Whid.Framework;
using Whid.Functional;
using Whid.Helpers;

namespace Whid
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private ISummaryService _service;
        private HighlightedSummaryList _highlightedEncompassedSummaries = new HighlightedSummaryList();

        public RelayCommand ShowSmallerSummariesCommand { get; }
        public RelayCommand ShowBiggerSummariesCommand { get; }
        public RelayCommand CreateNewSummaryCommand { get; }

        public MainWindowViewModel(ISummaryService service)
        {
            _service = service;

            SummaryCreation = new SummaryCreationModel { PeriodTime = DateTime.Now, PeriodType = PeriodType.FromTypeEnum(PeriodTypeEnum.Day) };
            ShowSmallerSummariesCommand = new RelayCommand(ShowSmallerSummaries, () => MainSummaryType.Encompasses?.EncompassesOthers ?? false);
            ShowBiggerSummariesCommand = new RelayCommand(ShowBiggerSummaries, () => MainSummaryType.IsEncompassedByOthers);
            CreateNewSummaryCommand = new RelayCommand(CreateNewSummary);

            ShowSummaries(PeriodTypeEnum.Month);
        }

        public List<PeriodType> PeriodTypes { get; set; } = PeriodType.AllPeriodTypes().ToList();

        private SummaryCreationModel summaryCreation;
        public SummaryCreationModel SummaryCreation
        {
            get => summaryCreation;
            set => SetProperty(ref summaryCreation, value);
        }

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
                selectedSummary.HighlightSummary(false);
                SetProperty(ref selectedSummary, value);
                selectedSummary.HighlightSummary(true);

                HighlightEncompassedSummaries(selectedSummary);
            }
        }

        private SummaryModel selectedEncompassedSummary;
        public SummaryModel SelectedEncompassedSummary
        {
            get => selectedEncompassedSummary;
            set
            {
                selectedEncompassedSummary.HighlightSummary(false);
                SetProperty(ref selectedEncompassedSummary, value);
                selectedEncompassedSummary.HighlightSummary(true);

                _highlightedEncompassedSummaries.ReplaceSummariesWith(selectedEncompassedSummary);
            }
        }

        private void HighlightEncompassedSummaries(SummaryModel summary)
        {
            _highlightedEncompassedSummaries.ReplaceSummariesWith(
                EncompassedSummaries.Where(s => summary?.PartiallyIncludes(s) ?? false));

            selectedEncompassedSummary = _highlightedEncompassedSummaries.FirstSummary().ReduceToDefault();
            OnPropertyChanged(nameof(SelectedEncompassedSummary));
        }

        private void ShowSummaries(PeriodTypeEnum type)
        {
            MainSummaryType = PeriodType.FromTypeEnum(type);
            ShowBiggerSummariesCommand.RaiseCanExecuteChanged();
            ShowSmallerSummariesCommand.RaiseCanExecuteChanged();

            Summaries = new ObservableCollection<SummaryModel>(GetSummariesOfType(MainSummaryType));
            EncompassedSummaries = new ObservableCollection<SummaryModel>(GetSummariesOfType(MainSummaryType.Encompasses));
        }

        private List<SummaryModel> GetSummariesOfType(PeriodType type) =>
            _service
                .GetSummaries()
                .OfSummaryType(type)
                .OrderBy(s => s.Period.DateRange.StartTime)
                .Select(s => s.ToViewModel(_service, DeleteSummary))
                .ToList();


        private void ShowSmallerSummaries() =>
            ShowSummaries(MainSummaryType.Encompasses.Type);

        private void ShowBiggerSummaries() =>
            ShowSummaries(MainSummaryType.EncompassedBy.Type);

        private void CreateNewSummary()
        {
            var summary = SummaryCreation.ConvertToSummary();
            var createdSummary = _service.SaveSummary(summary);
            ShowSummaries(createdSummary.Period.Type.Type);
            SelectedSummary = Summaries.SingleOrDefault(s => s.Id == createdSummary.Id);
        }

        private void DeleteSummary(SummaryModel summary)
        {
            if (null == summary)
            {
                return;
            }

            _service.DeleteSummary(summary.Id);
            RemoveSummaryFromView(summary);
        }

        private void RemoveSummaryFromView(SummaryModel summary)
        {
            if (summary.Period.Type == MainSummaryType)
            {
                Summaries.Remove(summary);
            }
            else
            {
                EncompassedSummaries.Remove(summary);
            }
        }
    }
}