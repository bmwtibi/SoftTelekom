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
            DataService.GetUserData((data, ok) =>
            {
                if (ok)
                {
                    UserInfo = data;
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

        }

        private UserData _userInfo;
        public UserData UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; RaisePropertyChanged(() => UserInfo); }
        }

        

    }
}