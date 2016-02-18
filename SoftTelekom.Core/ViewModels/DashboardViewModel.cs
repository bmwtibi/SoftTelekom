using Cirrious.CrossCore;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Messenger;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Helpers;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels.Bases;

namespace SoftTelekom.Core.ViewModels
{
    public class DashboardViewModel : MainViewModel
    {
        private MvxSubscriptionToken _tokenLang;
        public DashboardViewModel(IViewModelParams param) : base(param)
        {
            param.Builder.LoadResources(Settings.SavedLanguages == LanguagesEnum.HU ? "Hungarian" : "English");
            _tokenLang = Mvx.Resolve<IMvxMessenger>().Subscribe<LanguageChangeMessage>(message =>
                param.Builder.LoadResources(Settings.SavedLanguages == LanguagesEnum.HU ? "Hungarian" : "English"));
            
            Initialize();  
        }
        #region [ Private methods ]
        private void Initialize()
        {
            var vmFinder = ViewModelFinder.Instance;
            Menu = vmFinder.GetViewModel<MenuViewModel>();
            News = vmFinder.GetViewModel<NewsViewModel>();
            Order = vmFinder.GetViewModel<OrderViewModel>();
            Contact = vmFinder.GetViewModel<ContactViewModel>();
            Administration = vmFinder.GetViewModel<AdministrationViewModel>();
            SettingsVm = vmFinder.GetViewModel<SettingsViewModel>();
            User = vmFinder.GetViewModel<UserInfoViewModel>();
            Bill = vmFinder.GetViewModel<BillingInfoViewModel>();
            Usage = vmFinder.GetViewModel<InternetUsageViewModel>();
            Webmail = vmFinder.GetViewModel<WebmailViewModel>();
            Report = vmFinder.GetViewModel<ReportViewModel>();

        }

        #endregion [ Private methods ]

        #region [ Properties ]

        private NewsViewModel _news;
        public NewsViewModel News
        {
            get { return _news; }
            set { _news = value; RaisePropertyChanged(() => News); }
        }

        private MenuViewModel _menu;
        public MenuViewModel Menu
        {
            get { return _menu; }
            set { _menu = value; RaisePropertyChanged(() => Menu); }
        }

        private OrderViewModel _order;
        public OrderViewModel Order
        {
            get { return _order; }
            set { _order = value; RaisePropertyChanged(() => Order); }
        }
        private ContactViewModel _contact;
        public ContactViewModel Contact
        {
            get { return _contact; }
            set { _contact = value; RaisePropertyChanged(() => Contact); }
        }
        private AdministrationViewModel _administration;
        public AdministrationViewModel Administration
        {
            get { return _administration; }
            set { _administration = value; RaisePropertyChanged(() => Administration); }
        }

        private SettingsViewModel _settings;
        public SettingsViewModel SettingsVm
        {
            get { return _settings; }
            set { _settings = value; RaisePropertyChanged(() => SettingsVm); }
        }

        private UserInfoViewModel _user;
        public UserInfoViewModel User
        {
            get { return _user; }
            set { _user = value; RaisePropertyChanged(() => User); }
        }

        private BillingInfoViewModel _bill;
        public BillingInfoViewModel Bill
        {
            get { return _bill; }
            set { _bill = value; RaisePropertyChanged(() => Bill); }
        }

        private InternetUsageViewModel _usage;
        public InternetUsageViewModel Usage
        {
            get { return _usage; }
            set { _usage = value; RaisePropertyChanged(() => Usage); }
        }

        private WebmailViewModel _webmail;
        public WebmailViewModel Webmail
        {
            get { return _webmail; }
            set { _webmail = value; RaisePropertyChanged(() => Webmail); }
        }

        private ReportViewModel _report;
        public ReportViewModel Report
        {
            get { return _report; }
            set { _report = value; RaisePropertyChanged(() => Report); }
        }


        #endregion [ Properties ]

    }
}