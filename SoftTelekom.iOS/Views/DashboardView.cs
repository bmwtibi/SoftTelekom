using System;
using System.Drawing;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Touch.Views;
using Foundation;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Helpers;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.iOS.Views.CustomViews;
using UIKit;

namespace SoftTelekom.iOS.Views
{
    [Register("UniversalView")]
    public class UniversalView : UIView
    {
        public UniversalView()
        {
            Initialize();
        }

        public UniversalView(RectangleF bounds)
            : base(bounds)
        {
            Initialize();
        }

        void Initialize()
        {
            BackgroundColor = UIColor.Black;
        }
    }
    [Register("DashboardView")]
    public class DashboardView : MvxViewController
    {
        public DashboardView()
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public DashboardViewModel Model
        {
            get { return ViewModel as DashboardViewModel; }
        }

        private MvxSubscriptionToken _token;
        private MvxSubscriptionToken _menuToken;
        private SlideoutNavigationController _menu;

        public override void ViewDidLoad()
        {
            ViewModel = new DashboardViewModel(Mvx.Resolve<IViewModelParams>());
            base.ViewDidLoad();

            View = new UniversalView((RectangleF)UIScreen.MainScreen.Bounds);

            _menu = new SlideoutNavigationController();
            _menu.TopView = new NewsView() { ViewModel = Model.News };
            var menuView = new MenuView() { ViewModel = Model.Menu };
            _menu.MenuViewLeft = menuView;
            _menu.RightMenuEnabled = false;
            AddChildViewController(_menu);
            View.Add(_menu.View);
            _token = Mvx.Resolve<IMvxMessenger>().Subscribe<MenuItemSelectedMessage>(message => ShowScreen(message.MenuIndex));

            _menuToken = Mvx.Resolve<IMvxMessenger>().Subscribe<MenuMessage>(message =>
            {
                if (message.Navigation == NavigationEnum.Close)
                {
                    _menu.Hide();
                }
            });
            NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;


        }



        private void ShowScreen(int index)
        {
            switch (index)
            {
                case 0:
                    {
                        _menu.TopView = new NewsView() { ViewModel = Model.News };
                        break;
                    }
                case 1:
                    {
                        _menu.TopView = new OrderView() { ViewModel = Model.Order };
                        break;
                    }
                case 2:
                    {
                        _menu.TopView = new ContactView() { ViewModel = Model.Contact };
                        break;
                    }
                case 3:
                    {
                        _menu.TopView = new AdministrationView() { ViewModel = Model.Administration };
                        break;
                    }  
                case 4:
                    {
                        if (string.IsNullOrEmpty(Settings.SavedUser))
                        {
                            _menu.TopView = new SettingsView() { ViewModel = Model.SettingsVm };
                        }
                        else
                        {
                            _menu.TopView = new UserInfoView() {ViewModel = Model.User};
                        }
                        
                        break;
                    }
                case 5:
                    {
                        if (string.IsNullOrEmpty(Settings.SavedUser))
                        {
                            _menu.TopView = new NewsView() { ViewModel = Model.News };
                        }
                        else
                        {
                            _menu.TopView = new BillingInfoView() { ViewModel = Model.Bill };
                        }
                        break;
                    }
                case 6:
                    {
                        if (string.IsNullOrEmpty(Settings.SavedUser))
                        {
                            _menu.TopView = new NewsView() { ViewModel = Model.News };
                        }
                        else
                        {
                            _menu.TopView = new InternetUsageView() { ViewModel = Model.Usage };
                        }
                        break;
                    }
                case 7:
                    {
                        if (string.IsNullOrEmpty(Settings.SavedUser))
                        {
                            _menu.TopView = new NewsView() { ViewModel = Model.News };
                        }
                        else
                        {
                            _menu.TopView = new WebmailView() { ViewModel = Model.Webmail };
                        }
                        break;
                    }
                case 8:
                    {
                        if (string.IsNullOrEmpty(Settings.SavedUser))
                        {
                            _menu.TopView = new NewsView() { ViewModel = Model.News };
                        }
                        else
                        {
                            _menu.TopView = new ReportView() { ViewModel = Model.Report };
                        }
                        break;
                    }
                case 9:
                    {
                        if (string.IsNullOrEmpty(Settings.SavedUser))
                        {
                            _menu.TopView = new NewsView() { ViewModel = Model.News };
                        }
                        else
                        {
                            _menu.TopView = new SettingsView() { ViewModel = Model.SettingsVm }; ;
                        }
                        break;
                    }  
                default:
                    {
                        _menu.TopView = new NewsView() { ViewModel = Model.News };
                        break;
                    }
            }

        }
    }
}