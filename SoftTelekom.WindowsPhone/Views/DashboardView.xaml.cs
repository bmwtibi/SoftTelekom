using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.WindowsPhone.Views;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.WindowsPhone.Framework;

namespace SoftTelekom.WindowsPhone.Views
{
    public partial class DashboardView : MvxPhonePage
    {
        private DashboardViewModel Model
        {
            get { return (this.DataContext as DashboardViewModel); }
        }

        private const double DragDistanceToOpen = 150.0;
        private const double DragDistanceToClose = 40.0;
        private const double DragDistanceNegative = -40.0;

        private MvxSubscriptionToken _token;
        private MvxSubscriptionToken _menuToken;

        private bool _isSettingsOpen = false;
        private bool _isSettingsOpenRight = false;

        private readonly FrameworkElement _feContainer;

        public DashboardView()
        {
            InitializeComponent();

            _feContainer = this.Container as FrameworkElement;

            AdministrationView.Visibility = Visibility.Collapsed;
            BillingInfoView.Visibility = Visibility.Collapsed;
            ContactView.Visibility = Visibility.Collapsed;
            InternetUsageView.Visibility = Visibility.Collapsed;
            NewsView.Visibility = Visibility.Collapsed;
            OrderView.Visibility = Visibility.Collapsed;
            ReportView.Visibility = Visibility.Collapsed;
            SettingsView.Visibility = Visibility.Collapsed;
            UserInfoView.Visibility = Visibility.Collapsed;
            WebmailView.Visibility = Visibility.Collapsed;

            SubscriptionMessages();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            StartView(0);
        }

        private void StartView(int menuIndex)
        {
            switch (menuIndex)
            {
                case 0:
                    AdministrationView.Visibility = Visibility.Collapsed;
                    BillingInfoView.Visibility = Visibility.Collapsed;
                    ContactView.Visibility = Visibility.Collapsed;
                    InternetUsageView.Visibility = Visibility.Collapsed;
                    NewsView.Visibility = Visibility.Visible;
                    OrderView.Visibility = Visibility.Collapsed;
                    ReportView.Visibility = Visibility.Collapsed;
                    SettingsView.Visibility = Visibility.Collapsed;
                    UserInfoView.Visibility = Visibility.Collapsed;
                    WebmailView.Visibility = Visibility.Collapsed;

                    HeaderTextBlock.Text = Model.News.TopBarTitle;
                    break;

                case 1:
                    AdministrationView.Visibility = Visibility.Collapsed;
                    BillingInfoView.Visibility = Visibility.Collapsed;
                    ContactView.Visibility = Visibility.Collapsed;
                    InternetUsageView.Visibility = Visibility.Collapsed;
                    NewsView.Visibility = Visibility.Collapsed;
                    OrderView.Visibility = Visibility.Visible;
                    ReportView.Visibility = Visibility.Collapsed;
                    SettingsView.Visibility = Visibility.Collapsed;
                    UserInfoView.Visibility = Visibility.Collapsed;
                    WebmailView.Visibility = Visibility.Collapsed;

                    HeaderTextBlock.Text = Model.Order.TopBarTitle;
                    break;

                case 2:
                    AdministrationView.Visibility = Visibility.Collapsed;
                    BillingInfoView.Visibility = Visibility.Collapsed;
                    ContactView.Visibility = Visibility.Visible;
                    InternetUsageView.Visibility = Visibility.Collapsed;
                    NewsView.Visibility = Visibility.Collapsed;
                    OrderView.Visibility = Visibility.Collapsed;
                    ReportView.Visibility = Visibility.Collapsed;
                    SettingsView.Visibility = Visibility.Collapsed;
                    UserInfoView.Visibility = Visibility.Collapsed;
                    WebmailView.Visibility = Visibility.Collapsed;

                    HeaderTextBlock.Text = Model.Contact.TopBarTitle;
                    break;

                case 3:
                    AdministrationView.Visibility = Visibility.Visible;
                    BillingInfoView.Visibility = Visibility.Collapsed;
                    ContactView.Visibility = Visibility.Collapsed;
                    InternetUsageView.Visibility = Visibility.Collapsed;
                    NewsView.Visibility = Visibility.Collapsed;
                    OrderView.Visibility = Visibility.Collapsed;
                    ReportView.Visibility = Visibility.Collapsed;
                    SettingsView.Visibility = Visibility.Collapsed;
                    UserInfoView.Visibility = Visibility.Collapsed;
                    WebmailView.Visibility = Visibility.Collapsed;

                    HeaderTextBlock.Text = Model.Administration.TopBarTitle;
                    break;
                case 4:
                    AdministrationView.Visibility = Visibility.Collapsed;
                    BillingInfoView.Visibility = Visibility.Collapsed;
                    ContactView.Visibility = Visibility.Collapsed;
                    InternetUsageView.Visibility = Visibility.Collapsed;
                    NewsView.Visibility = Visibility.Collapsed;
                    OrderView.Visibility = Visibility.Collapsed;
                    ReportView.Visibility = Visibility.Collapsed;
                    SettingsView.Visibility = Visibility.Collapsed;
                    UserInfoView.Visibility = Visibility.Visible;
                    WebmailView.Visibility = Visibility.Collapsed;

                    HeaderTextBlock.Text = Model.User.TopBarTitle;
                    break;
                case 5:
                    AdministrationView.Visibility = Visibility.Collapsed;
                    BillingInfoView.Visibility = Visibility.Visible;
                    ContactView.Visibility = Visibility.Collapsed;
                    InternetUsageView.Visibility = Visibility.Collapsed;
                    NewsView.Visibility = Visibility.Collapsed;
                    OrderView.Visibility = Visibility.Collapsed;
                    ReportView.Visibility = Visibility.Collapsed;
                    SettingsView.Visibility = Visibility.Collapsed;
                    UserInfoView.Visibility = Visibility.Collapsed;
                    WebmailView.Visibility = Visibility.Collapsed;

                    HeaderTextBlock.Text = Model.Bill.TopBarTitle;
                    break;
                case 6:
                    AdministrationView.Visibility = Visibility.Collapsed;
                    BillingInfoView.Visibility = Visibility.Collapsed;
                    ContactView.Visibility = Visibility.Collapsed;
                    InternetUsageView.Visibility = Visibility.Visible;
                    NewsView.Visibility = Visibility.Collapsed;
                    OrderView.Visibility = Visibility.Collapsed;
                    ReportView.Visibility = Visibility.Collapsed;
                    SettingsView.Visibility = Visibility.Collapsed;
                    UserInfoView.Visibility = Visibility.Collapsed;
                    WebmailView.Visibility = Visibility.Collapsed;

                    HeaderTextBlock.Text = Model.Usage.TopBarTitle;
                    break;
                case 7:
                    AdministrationView.Visibility = Visibility.Collapsed;
                    BillingInfoView.Visibility = Visibility.Collapsed;
                    ContactView.Visibility = Visibility.Collapsed;
                    InternetUsageView.Visibility = Visibility.Collapsed;
                    NewsView.Visibility = Visibility.Collapsed;
                    OrderView.Visibility = Visibility.Collapsed;
                    ReportView.Visibility = Visibility.Collapsed;
                    SettingsView.Visibility = Visibility.Collapsed;
                    UserInfoView.Visibility = Visibility.Collapsed;
                    WebmailView.Visibility = Visibility.Visible;

                    HeaderTextBlock.Text = Model.Webmail.TopBarTitle;
                    break;
                case 8:
                    AdministrationView.Visibility = Visibility.Collapsed;
                    BillingInfoView.Visibility = Visibility.Collapsed;
                    ContactView.Visibility = Visibility.Collapsed;
                    InternetUsageView.Visibility = Visibility.Collapsed;
                    NewsView.Visibility = Visibility.Collapsed;
                    OrderView.Visibility = Visibility.Collapsed;
                    ReportView.Visibility = Visibility.Visible;
                    SettingsView.Visibility = Visibility.Collapsed;
                    UserInfoView.Visibility = Visibility.Collapsed;
                    WebmailView.Visibility = Visibility.Collapsed;

                    HeaderTextBlock.Text = Model.Report.TopBarTitle;
                    break;
                case 9:
                    AdministrationView.Visibility = Visibility.Collapsed;
                    BillingInfoView.Visibility = Visibility.Collapsed;
                    ContactView.Visibility = Visibility.Collapsed;
                    InternetUsageView.Visibility = Visibility.Collapsed;
                    NewsView.Visibility = Visibility.Collapsed;
                    OrderView.Visibility = Visibility.Collapsed;
                    ReportView.Visibility = Visibility.Collapsed;
                    SettingsView.Visibility = Visibility.Visible;
                    UserInfoView.Visibility = Visibility.Collapsed;
                    WebmailView.Visibility = Visibility.Collapsed;

                    HeaderTextBlock.Text = Model.SettingsVm.TopBarTitle;
                    break;
            }
        }

        private void SubscriptionMessages()
        {
            _token = Mvx.Resolve<IMvxMessenger>().Subscribe<MenuItemSelectedMessage>(message =>
            {
                StartView(message.MenuIndex);
            });

            _menuToken = Mvx.Resolve<IMvxMessenger>().Subscribe<MenuMessage>(message =>
            {
                if (message.Direction == DirectionEnum.Left && message.Navigation == NavigationEnum.Close)
                {
                    CloseSettings();
                }
                else if (message.Direction == DirectionEnum.Left && message.Navigation == NavigationEnum.Open)
                {
                    OpenSettings();
                }
            });
        }

        private void GestureListener_OnDragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            if (e.Direction == System.Windows.Controls.Orientation.Horizontal && e.HorizontalChange > 0 &&
                !_isSettingsOpen && !_isSettingsOpenRight)
            {
                double offset = _feContainer.GetHorizontalOffset().Value + e.HorizontalChange;
                if (offset > DragDistanceToOpen)
                    this.OpenSettings();
                else
                    _feContainer.SetHorizontalOffset(offset);
            }
            if (e.Direction == System.Windows.Controls.Orientation.Horizontal && e.HorizontalChange < 0 &&
                _isSettingsOpen)
            {
                double offsetContainer = _feContainer.GetHorizontalOffset().Value + e.HorizontalChange;
                if (offsetContainer < DragDistanceToClose)
                    this.CloseSettings();
                else
                    _feContainer.SetHorizontalOffset(offsetContainer);
            }
        }

        private void GestureListener_OnDragCompleted(object sender, DragCompletedGestureEventArgs e)
        {
            if (e.Direction == System.Windows.Controls.Orientation.Horizontal && e.HorizontalChange > 0 &&
                !_isSettingsOpen && !_isSettingsOpenRight)
            {
                if (e.HorizontalChange < DragDistanceToOpen)
                    this.ResetLayoutRoot();
                else
                    this.OpenSettings();
            }
            if (e.Direction == System.Windows.Controls.Orientation.Horizontal && e.HorizontalChange < 0 &&
                _isSettingsOpen)
            {
                if (e.HorizontalChange > DragDistanceNegative)
                    this.ResetLayoutRoot();
                else
                    this.CloseSettings();
            }
        }

        private void ResetLayoutRoot()
        {
            if (!_isSettingsOpen)
                _feContainer.SetHorizontalOffset(0.0);
            else
                _feContainer.SetHorizontalOffset(420.0);
        }

        private void ResetLayoutRootRight()
        {
            if (!_isSettingsOpenRight)
                _feContainer.SetHorizontalOffset(0.0);
            else
                _feContainer.SetHorizontalOffset(-420.0);
        }

        private void CloseSettings()
        {
            var trans = _feContainer.GetHorizontalOffset().Transform;
            trans.Animate(trans.X, 0, TranslateTransform.XProperty, 300, 0, new CubicEase
            {
                EasingMode = EasingMode.EaseOut
            });

            _isSettingsOpen = false;
        }

        private void OpenSettings()
        {
            var trans = _feContainer.GetHorizontalOffset().Transform;
            trans.Animate(trans.X, 420, TranslateTransform.XProperty, 300, 0, new CubicEase
            {
                EasingMode = EasingMode.EaseOut
            });

            _isSettingsOpen = true;
        }

        private void LeftMenuListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                Model.Menu.ListItemClick.Execute(e.AddedItems[0]);
                ((ListBox)sender).SelectedIndex = -1;
            }
        }

        private void MenuButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isSettingsOpen)
            {
                CloseSettings();
            }
            else
            {
                OpenSettings();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            while (NavigationService.BackStack.Any())
            {
                while (NavigationService.CanGoBack)
                {
                    NavigationService.RemoveBackEntry();
                }
            }
        }
    }
}