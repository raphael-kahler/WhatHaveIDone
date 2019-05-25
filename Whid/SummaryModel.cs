using System;
using Whid.Domain;
using Whid.Domain.Dates;
using Whid.Framework;
using Whid.Helpers;

namespace Whid
{
    public class SummaryModel : BaseViewModel
    {
        private ISummaryService _service;
        private Action<SummaryModel> _removeSummary;
        private string originalContent;

        private Guid id;
        public Guid Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private SummaryPeriod period;
        public SummaryPeriod Period
        {
            get => period;
            set => SetProperty(ref period, value);
        }
        

        private string content;
        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }

        private bool highlighted;
        public bool Highlighted
        {
            get => highlighted;
            set => SetProperty(ref highlighted, value);
        }

        private bool editMode;
        public bool EditMode
        {
            get => editMode;
            set => SetProperty(ref editMode, value);
        }

        public RelayCommand StartEditCommand { get; }
        public RelayCommand QuitEditCommand { get; }
        public RelayCommand DeleteCommand { get; }
        public RelayCommand SaveCommand { get; }

        public SummaryModel(ISummaryService service, Action<SummaryModel> removeSummary)
        {
            _removeSummary = removeSummary;
            _service = service;
            StartEditCommand = new RelayCommand(SwitchToEditMode);
            QuitEditCommand = new RelayCommand(() => QuitEditMode(revert: true));
            DeleteCommand = new RelayCommand(DeleteSummary);
            SaveCommand = new RelayCommand(Save);
        }

        private void SwitchToEditMode()
        {
            originalContent = Content;
            Highlighted = false;
            EditMode = true;
        }

        private void Save()
        {
            _service.SaveSummary(this.ToDomainModel());
            QuitEditMode(revert: false);
        }

        private void QuitEditMode(bool revert)
        {
            if (revert)
            {
                Content = originalContent;
            }
            EditMode = false;
            Highlighted = true;
        }

        private void DeleteSummary()
        {
            _removeSummary(this);
        }

        public bool PartiallyIncludes(SummaryModel other) =>
            Period.DateRange.PartiallyIncludesDateRange(other.Period.DateRange);
    }
}
