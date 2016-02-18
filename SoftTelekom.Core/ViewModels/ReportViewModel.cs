using System;
using System.Linq;
using Cirrious.MvvmCross.ViewModels;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels.Bases;

namespace SoftTelekom.Core.ViewModels
{
    public class ReportViewModel : MainViewModel
    {
        public ReportViewModel(IViewModelParams param)
            : base(param)
        {
            TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("BugReport");
        }

        public MvxCommand SendCommand
        {
            get
            {
                return new MvxCommand(SendExecute);
            }
        }
        private void SendExecute()
        {

        }

        public object ReasonList
        {
            get
            {
                return from e in Enum.GetValues(typeof(ReportReasonEnum)).Cast<ReportReasonEnum>()
                       select e;
            }
        }

        private ReportReasonEnum _selectedReason;
        public ReportReasonEnum SelectedReason
        {
            get { return _selectedReason; }
            set { _selectedReason = value; RaisePropertyChanged(() => SelectedReason); }
        }

        public MvxCommand<ReportReasonEnum> SelectedReasonCommand
        {
            get
            {
                return new MvxCommand<ReportReasonEnum>(SelectedReasonExecute);
            }
        }
        private void SelectedReasonExecute(ReportReasonEnum item)
        {
            SelectedReason = item;
        }

        private string _descText;
        public string DescText
        {
            get { return _descText; }
            set { _descText = value; RaisePropertyChanged(() => DescText); }
        }
    }
}