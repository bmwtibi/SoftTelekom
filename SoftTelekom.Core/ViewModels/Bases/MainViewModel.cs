using Cirrious.CrossCore;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Localization;
using Cirrious.MvvmCross.Plugins.JsonLocalisation;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Helpers;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;

namespace SoftTelekom.Core.ViewModels.Bases
{
    public class MainViewModel : MvxViewModel
    {
        public MvxColor TopBarColor
        {
            get
            {
                return Settings.SavedTheme == ThemeEnum.Magenta ? new MvxColor(167, 74, 133) : new MvxColor(32, 56, 128);//(32, 42, 73)
            }
        }

        public MvxColor TopTitleColor { get {return new MvxColor(255,255,255);} }
        public MvxColor MenuBarColor { get { return Settings.SavedTheme == ThemeEnum.Magenta ? new MvxColor(167, 74, 133) : new MvxColor(32, 56, 128); } }
        public MvxColor PickerColor { get { return Settings.SavedTheme == ThemeEnum.Magenta ? new MvxColor(226, 201, 223) : new MvxColor(154, 197, 255); } }
        public MvxColor MenuTitleColor { get { return new MvxColor(255,255, 255); } }
        public MvxColor MenuItemsTextColor { get; set; }
        public ThemeEnum CurrenTheme { get { return Settings.SavedTheme; } }
        public string TopBarTitle { get; set; }

        public IMvxLanguageBinder TextSource
        {
            get { return new MvxLanguageBinder(Constants.GeneralNamespace, GetType().Name); }
        }
        public IMvxLanguageBinder SharedTextSource
        {
            get { return new MvxLanguageBinder(Constants.GeneralNamespace, Constants.Shared); }
        }
        protected IDialogService DialogService { get; set; }
        protected IMvxMessenger MessengerService { get; set; }
        protected IMvxTextProviderBuilder Builder { get; set; }
        protected INetworkService NetworkService { get; set; }
        protected PlatformEnum CurrentPlatform { get; set; }

        private MvxSubscriptionToken _tokenTheme;
        public MainViewModel()
        {

        }

        public MainViewModel(IViewModelParams viewModelParams)
        {
            _tokenTheme = Mvx.Resolve<IMvxMessenger>().Subscribe<ThemeChangeMessage>(message => {
                                                                                                    RaisePropertyChanged(() => MenuBarColor);
                                                                                                    RaisePropertyChanged(() => TopBarColor);
            }

    );
            DialogService = viewModelParams.DialogService;
            MessengerService = viewModelParams.MessengerService;
            Builder = viewModelParams.Builder;
            NetworkService = viewModelParams.NetworkService;
            CurrentPlatform = Mvx.Resolve<IPlatform>().ActivePlatform();
            
        }
        public class ViewModelParams : IViewModelParams
        {
            private readonly IDialogService _dialogService;
            private readonly IMvxMessenger _messengerService;
            private readonly IMvxTextProviderBuilder _builder;
            private readonly INetworkService _networkService;
            public ViewModelParams(IDialogService dialogService, IMvxMessenger messenger, IMvxTextProviderBuilder builder, INetworkService networkService)
            {
                _dialogService = dialogService;
                _messengerService = messenger;
                _builder = builder;
                _networkService = networkService;

            }

            public IDialogService DialogService
            {
                get { return _dialogService; }
            }

            public IMvxMessenger MessengerService
            {
                get { return _messengerService; }
            }
            public IMvxTextProviderBuilder Builder
            {
                get { return _builder; }
            }

            public INetworkService NetworkService
            {
                get { return _networkService; }
            }
        }
    }
}