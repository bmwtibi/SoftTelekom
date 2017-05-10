using System;
using Cirrious.MvvmCross.ViewModels;
using SoftTelekom.Core.Models.ServiceModel;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels.Bases;

namespace SoftTelekom.Core.ViewModels
{
    public class UserInfoViewModel : MainViewModel
    {
        public UserInfoViewModel(IViewModelParams param) : base(param)
        {
            TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("MyInfo");

        }

		public void LoadData()
		{
			DataServiceSingletone.Instance.Service.GetMyInfo((UserData myInfo, string errorMessage, bool isError) =>
			{
				if (isError || myInfo == null)
				{
					DialogService.ShowDialogBox("Figyelem!", "Sikertelen adatlekérés");
				}
				else
				{
					InvokeOnMainThread(() =>
					{
						UserInfo = myInfo;
					});
				}

			});
		}

        public MvxCommand SaveCommand
        {
            get
            {
                return new MvxCommand(SaveExecute);
            }
        }
        private void SaveExecute()
        {
			if (NetworkService.CheckInternet() == Enums.NetworkEnum.None)
			{
				DialogService.ShowDialogBox("Információ!", "Küldés sikertelen! Nincs internetkapcsolat!");
			}
			else {
				if (string.IsNullOrWhiteSpace(UserInfo.Name) || string.IsNullOrWhiteSpace(UserInfo.Email) || string.IsNullOrWhiteSpace(UserInfo.Address) || string.IsNullOrWhiteSpace(UserInfo.PhoneNumber) )
				{
					DialogService.ShowDialogBox(SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Error"), SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ErrorEmptyFieldRemaning"));
				}
				else 
				{ 
					DataServiceSingletone.Instance.Service.SaveMyInfo(UserInfo, (bool isSucces, string errorMessage, bool isError) =>
					 {
						 if (isSucces && !isError)
						 {
							 InvokeOnMainThread(() =>
							 {
								DialogService.ShowDialogBox("Figyelem", "Az adatok mentése sikeres!");

							 });
						 }
						 else
						 {
							 InvokeOnMainThread(() =>
							{
								DialogService.ShowDialogBox("Figyelem", "Az adatok mentése sikertelen!");

							});
						 }
					 });
				}
			}

        }

        private UserData _userInfo;
        public UserData UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; RaisePropertyChanged(() => UserInfo); }
        }

        

    }
}