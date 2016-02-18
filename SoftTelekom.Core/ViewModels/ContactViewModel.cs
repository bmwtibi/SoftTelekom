using Cirrious.CrossCore;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Email;
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
    public class ContactViewModel : MainViewModel
    {
        private MvxSubscriptionToken _tokenLang;
        public ContactViewModel(IViewModelParams param) : base(param)
        {          
            InitText();
            _tokenLang = Mvx.Resolve<IMvxMessenger>().Subscribe<LanguageChangeMessage>(message =>
            {
                param.Builder.LoadResources(Settings.SavedLanguages == LanguagesEnum.HU ? "Hungarian" : "English");
                InitText();
            });
        }

        #region [ Private methods ]
        public void InitText()
        {
            TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Contact");
            AddressLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactAddress");
            OpenLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactOpenHours");
            PhoneLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactPhone");
            SmsLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactSms");
            MobilePhoneLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactMobilePhone");
            FaxLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactFax");
            SkypeLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactSkype");
            EmailLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactEmail");
        }
        #endregion [ Private methods ] 

        #region [ Properties ]
        private string _addressLabel;
        public string AddressLabel
        {
            get { return _addressLabel; }
            set { _addressLabel = value; RaisePropertyChanged(() => AddressLabel); }
        }
        private string _openLabel;
        public string  OpenLabel
        {
            get { return _openLabel; }
            set { _openLabel = value; RaisePropertyChanged(() => OpenLabel); }
        }
        private string _phoneLabel;
        public string PhoneLabel
        {
            get { return _phoneLabel; }
            set { _phoneLabel = value; RaisePropertyChanged(() => PhoneLabel); }
        }

        private string _smsLabel;
        public string SmsLabel
        {
            get { return _smsLabel; }
            set { _smsLabel = value; RaisePropertyChanged(() => SmsLabel); }
        }
        private string _mobilePhoneLabel;
        public string MobilePhoneLabel
        {
            get { return _mobilePhoneLabel; }
            set { _mobilePhoneLabel = value; RaisePropertyChanged(() => MobilePhoneLabel); }
        }
        private string _faxLabel;
        public string FaxLabel
        {
            get { return _faxLabel; }
            set { _faxLabel = value; RaisePropertyChanged(() => FaxLabel); }
        }
        private string _skypeLabel;
        public string SkypeLabel
        {
            get { return _skypeLabel; }
            set { _skypeLabel = value; RaisePropertyChanged(() => SkypeLabel); }
        }
        private string _emailLabel;
        public string EmailLabel
        {
            get { return _emailLabel; }
            set { _emailLabel = value; RaisePropertyChanged(() => EmailLabel); }
        }
        private IMvxComposeEmailTask _email;

        protected IMvxComposeEmailTask Email
        {
            get
            {
                _email = _email ?? Mvx.Resolve<IMvxComposeEmailTask>();
                return _email;
            }
        }
        #endregion [ Properties ]

        #region [ Commands ]
        public MvxCommand EmailCommand
        {
            get
            {
                return new MvxCommand(EmailExecute);
            }
        }

        private void EmailExecute()
        {
            Email.ComposeEmail("kapcsolat@soft-telekom.hu", string.Empty, "Kapcsolat", string.Empty, false);
        }
        #endregion [ Commands ]

    }
}