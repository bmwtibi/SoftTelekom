using System.Collections.Generic;
using System.Xml;
using Cirrious.CrossCore;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Helpers;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.Models;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels.Bases;

namespace SoftTelekom.Core.ViewModels
{
    public class MenuViewModel : MainViewModel
    {
        private MvxSubscriptionToken _tokenLang;
        private MvxSubscriptionToken _tokenTheme;
        private MvxSubscriptionToken _tokenLogInOut;
        public MenuViewModel(IViewModelParams param)
            : base(param)
        {
            InitializeMenu();
            _tokenLang = Mvx.Resolve<IMvxMessenger>().Subscribe<LanguageChangeMessage>(message =>
            {
                param.Builder.LoadResources(Settings.SavedLanguages == LanguagesEnum.HU ? "Hungarian" : "English");
                InitializeMenu();
            });
            _tokenTheme = param.MessengerService.Subscribe<ThemeChangeMessage>(message => { RaisePropertyChanged(() => CurrenTheme); });
            _tokenLogInOut = Mvx.Resolve<IMvxMessenger>().Subscribe<LogInOutMessage>(message => InitializeMenu());
        }

        #region [ Private methods ]
        private void InitializeMenu()
        {
            if (string.IsNullOrEmpty(Settings.SavedUserEmail))
            {
                MenuItems = new List<MenuItem>()
                {
                    new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("News"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 0
                    },
                    new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Order"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 1
                    },

                    new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Contact"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 2
                    },
                    new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Administration"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 3
                    },
                    // TODO Átírtam 9re a settingset, hogy elég legyen Androidnál egy switch :D
                    new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Settings"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 9
                    },
                
                };
            }
            else
            {
                MenuItems = new List<MenuItem>()
                {
                    new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("News"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 0
                    },
                    new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Order"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 1
                    },

                    new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Contact"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 2
                    },
                    new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Administration"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 3
                    },
                    new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("MyInfo"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 4
                    },
                    new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("BillInfo"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 5
                    },
                     new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("InternetUsage"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 6
                    },
                     new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Webmail"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 7
                    },
                     new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("BugReport"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 8
                    },
                    new MenuItem()
                    {
                        Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Settings"),
                        ViewModelType = typeof (DashboardViewModel),
                        MenuTitle = false,
                        MenuIndex = 9
                    },
                
                };
            }
            MenuText = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Menu");

        }
        #endregion [ Private methods ]

        #region [ Properties ]
        private string _menuText;
        public string MenuText
        {
            get { return _menuText; }
            set { _menuText = value; RaisePropertyChanged(() => MenuText); }
        }
        private List<MenuItem> _menuItems;

        public List<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                RaisePropertyChanged(() => MenuItems);
            }
        }
        #endregion [ Properties ]

        #region [ Commands ]
        public MvxCommand<MenuItem> ListItemClick
        {
            get
            {
                return new MvxCommand<MenuItem>(ListItemClickExecute);
            }
        }

        private void ListItemClickExecute(MenuItem item)
        {
            MessengerService.Publish(new MenuItemSelectedMessage(this, item.ViewModelType, item.MenuIndex));
            MessengerService.Publish(new MenuMessage(this, NavigationEnum.Close, DirectionEnum.Left));
        }
        #endregion [ Commands ]

    }
}