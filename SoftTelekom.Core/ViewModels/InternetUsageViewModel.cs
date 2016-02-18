using System.Collections.ObjectModel;
using Cirrious.CrossCore.UI;
using SoftTelekom.Core.Models;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels.Bases;

namespace SoftTelekom.Core.ViewModels
{
    public class InternetUsageViewModel : MainViewModel
    {
        public InternetUsageViewModel(IViewModelParams param)
            : base(param)
        {
            TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("InternetUsage");
            DataService.GetCurrentDataUsage((data,ok) =>
            {
                if (ok)
                {
                    CurrentDataText = data;
                }
            });
            DataService.GetListDataUsage((data, ok) =>
            {
                if (ok)
                {
                    InternetUsageItemList = new ObservableCollection<InternetUsageItem>(data);
                }
            });
        }

        private string _currentDataText;
        public string CurrentDataText
        {
            get { return _currentDataText; }
            set { _currentDataText = value; RaisePropertyChanged(() => CurrentDataText); }
        }

        private ObservableCollection<InternetUsageItem> _internetUsageItemList;
        public ObservableCollection<InternetUsageItem> InternetUsageItemList
        {
            get { return _internetUsageItemList; }
            set
            {
                _internetUsageItemList = value;
                for (int i = 0; i < _internetUsageItemList.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        _internetUsageItemList[i].ListBackgroundColor = new MvxColor(233, 237, 238);
                    }
                    else
                    {
                        _internetUsageItemList[i].ListBackgroundColor = new MvxColor(242, 245, 245);
                    }
                }
                RaisePropertyChanged(() => InternetUsageItemList);
            }
        }
    }
}