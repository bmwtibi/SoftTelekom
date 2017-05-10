using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Helpers;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels.Bases;

namespace SoftTelekom.Core.ViewModels
{
    public class AdministrationViewModel : MainViewModel
    {
        private readonly IDialogService _dialog;
        private MvxSubscriptionToken _tokenLang;

        public AdministrationViewModel(IViewModelParams param) : base(param)
        {
            _dialog = param.DialogService;
            InitText();
            _tokenLang = Mvx.Resolve<IMvxMessenger>().Subscribe<LanguageChangeMessage>(message =>
            {
                param.Builder.LoadResources(Settings.SavedLanguages == LanguagesEnum.HU ? "Hungarian" : "English");
                InitText();
            });
            InputIsEnabled = string.IsNullOrEmpty(Settings.SavedUserEmail);
        }

        #region [ Private methods ]
        private void InitText()
        {
            TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Administration");
            OnlineDescription = string.IsNullOrEmpty(Settings.SavedUserEmail)
                ? SharedTextSourceSingleton.Instance.SharedTextSource.GetText("LogInDescription")
                : SharedTextSourceSingleton.Instance.SharedTextSource.GetText("LogOutDescription");
            LogInOutButtonTitle = string.IsNullOrEmpty(Settings.SavedUserEmail)
                ? SharedTextSourceSingleton.Instance.SharedTextSource.GetText("LogIn")
                : SharedTextSourceSingleton.Instance.SharedTextSource.GetText("LogOut");
			if (!string.IsNullOrEmpty(Settings.SavedUserEmail))
            {
                UserName = Settings.SavedUserEmail; 
            }
#if DEBUG
            UserName = "goretibor@yahoo.com";
            UserPwd = "asdqwe";
#endif
        }
        #endregion

        #region [ Properties ]

        private string _onlineDescription;
        public string OnlineDescription
        {
            get { return _onlineDescription; }
            set { _onlineDescription = value; RaisePropertyChanged(() => OnlineDescription); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged(() => UserName); }
        }

        private string _userPwd;
        public string UserPwd
        {
            get { return _userPwd; }
            set { _userPwd = value; RaisePropertyChanged(() => UserPwd); }
        }

        private bool _inputIsEnabled;
        public bool InputIsEnabled
        {
            get { return _inputIsEnabled; }
            set { _inputIsEnabled = value; RaisePropertyChanged(() => InputIsEnabled); }
        }


        private string _logInOutButtonTitle;
        public string LogInOutButtonTitle
        {
            get { return _logInOutButtonTitle; }
            set { _logInOutButtonTitle = value; RaisePropertyChanged(() => LogInOutButtonTitle); }
        }

        #endregion

        #region [ Commands ]
        public MvxCommand LogInCommand
        {
            get
            {
                return new MvxCommand(LogInExecute);
            }
        }
        private void LogInExecute()
        {
            if (string.IsNullOrEmpty(Settings.SavedUserEmail))
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    _dialog.ShowDialogBox(SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Error"), SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ErrorEmptyUserName"));
                }
                else if (string.IsNullOrEmpty(UserPwd))
                {
                    _dialog.ShowDialogBox(SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Error"), SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ErrorEmptyUserPwd"));
                }
                else
                {
					DataServiceSingletone.Instance.Service.Login(new LoginModel() { Email = UserName, Password = UserPwd }, 
					                                             (string token, string message, bool isError) => 
					{
						if (string.IsNullOrEmpty(token) || isError)
						{
							_dialog.ShowDialogBox(SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Error"), message);
						}
						else 
						{
							InvokeOnMainThread(() =>
							{
								MessengerService.Publish(new LogInOutMessage(this));
								InputIsEnabled = false;
								_dialog.ShowDialogBox(SharedTextSourceSingleton.Instance.SharedTextSource.GetText("SuccessEntry"), SharedTextSourceSingleton.Instance.SharedTextSource.GetText("SuccessEntryDesc"));
								OnlineDescription = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("LogOutDescription");
								LogInOutButtonTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("LogOut");
							});
						}


					});
                   
                    
                } 
            }
            else
            {    
                InputIsEnabled = true;
                UserName = string.Empty;
                UserPwd = string.Empty;
                Settings.SavedUserEmail = string.Empty;
				Settings.SavedUserToken = string.Empty;
                MessengerService.Publish(new LogInOutMessage(this));
                OnlineDescription =  SharedTextSourceSingleton.Instance.SharedTextSource.GetText("LogInDescription");
                LogInOutButtonTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("LogIn");
            }
            
        }
        #endregion
    }
}