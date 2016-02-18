using System.Collections.ObjectModel;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.ViewModels;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Models;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels.Bases;

namespace SoftTelekom.Core.ViewModels
{
    public class BillingInfoViewModel : MainViewModel
    {
        public BillingInfoViewModel(IViewModelParams param)
            : base(param)
        {
            TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("BillInfo");
            DataService.GetBillItem((list, ok) =>
            {
                if (ok)
                {
                    BillItemList = new ObservableCollection<BillItem>(list);
                }
            });
        }

        public MvxCommand<BillItem> SelectedBillItemCommand
        {
            get
            {
                return new MvxCommand<BillItem>(SelectedBillItemExecute);
            }
        }
        private void SelectedBillItemExecute(BillItem item)
        {
        
        }

        private ObservableCollection<BillItem> _billItemList;
        public ObservableCollection<BillItem> BillItemList
        {
            get { return _billItemList; }
            set
            {
                _billItemList = value;
                for (int i = 0; i < _billItemList.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        _billItemList[i].ListBackgroundColor = new MvxColor(233, 237, 238);
                    }
                    else
                    {
                        _billItemList[i].ListBackgroundColor = new MvxColor(242, 245, 245);
                    }

                    if (CurrentPlatform == PlatformEnum.Android)
                    {
                        if (_billItemList[i].IsPaid)
                        {
                            _billItemList[i].ListTextColor = new MvxColor(0, 128, 0);
                        }
                        else
                        {
                            _billItemList[i].ListTextColor = new MvxColor(255, 0, 0);
                        }
                    }
                }
                RaisePropertyChanged(() => BillItemList);
            }
        }
    }
}