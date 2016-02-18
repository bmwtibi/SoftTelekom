using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using CoreGraphics;
using Foundation;
using SoftTelekom.Core.Services;
using SoftTelekom.iOS.Services;
using SoftTelekom.iOS.Views.CustomViews;
using UIKit;

namespace SoftTelekom.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
    {
        UIWindow _window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            _window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, _window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            _window.MakeKeyAndVisible();

            

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Sound |
                                           UIUserNotificationType.Alert | UIUserNotificationType.Badge, null);

                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(UIRemoteNotificationType.Badge |
                                                                         UIRemoteNotificationType.Sound | UIRemoteNotificationType.Alert);
            }

            return true;
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            var oldDeviceToken = NSUserDefaults.StandardUserDefaults.StringForKey("PushDeviceToken");
            var newDeviceToken = deviceToken.ToString().Replace("<", "").Replace(">", "").Replace(" ", "");

            if (string.IsNullOrEmpty(oldDeviceToken) || !oldDeviceToken.Equals(newDeviceToken))
            {
                //TODO: Put your own logic here to notify your server that the device token has changed/been created!
            }

            //Save device token now
            NSUserDefaults.StandardUserDefaults.SetString(newDeviceToken, "PushDeviceToken");
            Console.WriteLine("-----------------------------------------Device Token: " + newDeviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            Console.WriteLine("---------------------------------------------Failed to register for notifications {0}",error.ToString());
        }

        private DialogService _dialog;
        private OwnProgressBar _ownProgressBar;
        private UIView _loadView;
        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            //UIApplication.SharedApplication.InvokeOnMainThread(() =>
            //{
            //    new UIAlertView("", "",null,null,null).Show();
            //});
            _ownProgressBar = new OwnProgressBar();
            _dialog = new DialogService();
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
            foreach (var value in userInfo)
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine(string.Format("{0} {1}", value.Key, value.Value));
                Console.WriteLine("-----------------------------------------------");
            }

            string pushInformation = userInfo.FirstOrDefault(x => x.Key.Description == "aps").Value.ToString();
            Console.WriteLine("-----pushInformation-----" + pushInformation + "----------------------"); //{alert = "newReg|Users|8936303411050197912F|true|Valaki|36701234567|1D5D7A5CA8A0E4C744ED5BA6E51F07103CE76099|NOKIARM-892_eu_euro2_270";badge = 1;sound = "new_registration.caf";}

            NSObject pushInformationItem = userInfo.FirstOrDefault(x => x.Key.Description == "aps").Value;

            string alertInfo = pushInformationItem.ValueForKey(new NSString("alert")).ToString();
            Console.WriteLine("----alertInfo------" + alertInfo + "----------------------"); // newReg|Users|8936303411050197912F|true|Valaki|36701234567|1D5D7A5CA8A0E4C744ED5BA6E51F07103CE76099|NOKIARM-892_eu_euro2_270

            StringBuilder message = new StringBuilder();

            if (alertInfo.Contains("load1"))
            {
                _dialog.ShowProgressBar("prog");
            }
            if (alertInfo.Contains("load2"))
            {
                _loadView = new UIView(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height)) { BackgroundColor = new UIColor(0, 0, 0, 0.4f) };
                _window.Add(new UIView(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height)) { BackgroundColor = new UIColor(0, 0, 0, 0.4f) });
            }
            //Mvx.Resolve<IDialogService>().ShowProgressBar(SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Processing"));
            
            message.AppendLine(AddGateControl());
            wair();
            
            

            //TODO adatok feldolgozása
            Console.WriteLine("------------------------------------------Received Remote Notification!");
        }

        private  string AddGateControl()
        {

            
            
            return "valami";

        }

        private async void wair()
        {
            await Task.Delay(2000);
            var alert = new UIAlertView();


            alert.Title = "kész";

            alert.Message = "kész";

            alert.AddButton("Ok");
            alert.Clicked += (sender, args) =>
            {
                _dialog.HideProgressBar();
            };

            alert.Show();
        }
    }
}