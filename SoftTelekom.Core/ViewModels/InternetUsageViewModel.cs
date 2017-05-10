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
            
        }

		public void LoadData()
		{
			DataServiceSingletone.Instance.Service.GetDataUsage((DataUsageResponseModel myDataUsage, string errorMessage, bool isError) =>
			{
				if (isError || myDataUsage == null)
				{
					DialogService.ShowDialogBox("Figyelem!", "Sikertelen adatlekérés");
				}
				else
				{
					InvokeOnMainThread(() =>
					{
						CurrentDataTodayText = myDataUsage.TodayData;

						CurrentDataText = myDataUsage.CurrentMonthData;

						InternetUsageItemList = new ObservableCollection<InternetUsageItem>(myDataUsage.OldData);
					});
				}

			});
		}

        private int _currentDataText;
        public int CurrentDataText
        {
            get { return _currentDataText; }
            set { _currentDataText = value; RaisePropertyChanged(() => CurrentDataText); }
        }

		private int _currentDataTodayText;
		public int CurrentDataTodayText
		{
			get { return _currentDataTodayText; }
			set { _currentDataTodayText = value; RaisePropertyChanged(() => CurrentDataTodayText); }
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