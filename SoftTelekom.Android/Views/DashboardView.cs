using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using SlidingMenuSharp;
using SoftTelekom.Android.Utils;
using SoftTelekom.Android.Views.Bases;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.ViewModels;

namespace SoftTelekom.Android.Views
{

    [Activity(ScreenOrientation = ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.StateAlwaysHidden, Theme = "@android:style/Theme.Holo.Light.NoActionBar")]
    public class DashboardView : DashboardBaseActivity
    {
        // ReSharper disable NotAccessedField.Local
        private MvxSubscriptionToken _token;
        private MvxSubscriptionToken _menuToken;
        // ReSharper restore NotAccessedField.Local

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.DashboardView);
            //MenuImageView.Click += (sender, args) => ShowMenu();
            IMvxMessenger mvxMessenger = Mvx.Resolve<IMvxMessenger>();
            _token = mvxMessenger.Subscribe<MenuItemSelectedMessage>(message => ShowFragment(message.MenuIndex));
            _menuToken = mvxMessenger.Subscribe<MenuMessage>(message => CloseMenuIfNeeded(message.Navigation));
            ShowFragment(0);
        }

        public override void OnBackPressed()
        {
            //DialogHelper.ShowConfirmDialogBox(this, Model.SharedTextSource.GetText("ExitHeader"), Model.SharedTextSource.GetText("ExitText"), DialogHelper.OkButtonText, DialogHelper.CancelButtonText, b => { if (b) Finish(); });
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (IsApplicationBroughtToBackground()) Finish();
        }

        private bool IsApplicationBroughtToBackground()
        {
            ActivityManager am = GetSystemService(ActivityService).JavaCast<ActivityManager>();
            IList<ActivityManager.RunningTaskInfo> tasks = am.GetRunningTasks(1);
            if (tasks!= null)
            {
                ComponentName topActivity = tasks[0].TopActivity;
                CustomToast.ShowToast(this, "TopActivity: " + topActivity.PackageName, "Current: " + PackageName);
                if (topActivity.PackageName != PackageName)
                {
                    return true;
                }
            }

            return false;
        }

        private void ShowFragment(int menuIndex)
        {
            //0SlidingMenu.TouchModeAbove = menuIndex == 1 ? TouchMode.Margin : TouchMode.Fullscreen;
            switch (menuIndex)
            {
                case 0: SetActiveFragment(new NewsView(), Model.News);
                    break;
                case 1: SetActiveFragment(new OrderView(), Model.Order);
                    break;
                case 2: SetActiveFragment(new ContactView(), Model.Contact);
                    break;
                case 3: SetActiveFragment(new AdministrationView(), Model.Administration);
                    break;
                case 4: SetActiveFragment(new UserInfoView(), Model.User);
                    break;
                case 5: SetActiveFragment(new BillingInfoView(), Model.Bill);
                    break;
                case 6: SetActiveFragment(new InternetUsageView(), Model.Usage);
                    break;
                case 7: SetActiveFragment(new WebmailView(), Model.Webmail);
                    break;
                case 8: SetActiveFragment(new ReportView(), Model.Report);
                    break;
                case 9: SetActiveFragment(new SettingsView(), Model.SettingsVm);
                    break;
                
            }
        }

        private void CloseMenuIfNeeded(NavigationEnum message)
        {
            if (message == NavigationEnum.Close && SlidingMenu.IsMenuShowing) SlidingMenu.Toggle(true);
        }
    }

}