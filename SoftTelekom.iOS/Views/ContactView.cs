using System;
using System.Security.Policy;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.ViewModels;
using CoreAnimation;
using Foundation;
using MessageUI;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.iOS.Utils;
using SoftTelekom.iOS.Views.Controls;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views
{
    [Register("ContactView")]
    public class ContactView : MvxViewController
    {
        private IDialogService _dialog;

        public ViewGroup Layout;

        private NativeView _addressButtonView;

        private UILabel _addressLabel;
        private UILabel _openLabel;
        private UILabel _phoneLabel;
        private UILabel _smsLabel;
        private UILabel _mobilePhoneLabel;
        private UILabel _faxLabel;
        private UILabel _skypeLabel;
        private UILabel _emailLabel;
        protected ContactViewModel Model
        {
            get
            {
                return ViewModel as ContactViewModel;
            }
        }

        public override void ViewDidLoad()
        {
            //TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Contact");
            //AddressLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactAddress");
            //OpenLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactOpenHours");
            //PhoneLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactPhone");
            //SmsLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactSms");
            //MobilePhoneLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactMobilePhone");
            //FaxLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactFax");
            //SkypeLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactSkype");
            //EmailLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ContactEmail");
            base.ViewDidLoad();

            #region [Control Elements]
            _dialog = Mvx.Resolve<IDialogService>();
            UIPasteboard gpBoard = UIPasteboard.General;
            var addressControl = new ButtonControl(Helper.GetLangText("ContactAddress"), () =>
            {
                _dialog.ShowDialogBox(SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Office"), SharedTextSourceSingleton.Instance.SharedTextSource.GetText("OfficeClipboard"));
                gpBoard.String = "4625 Záhony, Ifjúság út 3/A";
            }) { LabelFontColor = UIColor.White, Margin = new UIEdgeInsets(10, 10, 0, 10),InsideMargin = new UIEdgeInsets(10,5,10,5),Height = 0};
            var openControl = new ButtonControl(Helper.GetLangText("ContactOpenHours"), () =>
            {
                
            }) { LabelFontColor = UIColor.White, Margin = new UIEdgeInsets(5, 10, 0, 10), InsideMargin = new UIEdgeInsets(10, 5, 10, 5), Height = 0 };
            var phoneControl = new ButtonControl(Helper.GetLangText("ContactPhone"), () =>
            {
                
            }) { LabelFontColor = UIColor.White, Margin = new UIEdgeInsets(5, 10, 0, 10), InsideMargin = new UIEdgeInsets(10, 5, 10, 5), Height = 0 };
            var smsControl = new ButtonControl(Helper.GetLangText("ContactSms"), () =>
            {
                var smsController = new MFMessageComposeViewController();
                if (MFMessageComposeViewController.CanSendText)
                {
                    smsController.Body = "";
                    var tmp = new string[1];
                    tmp[0] = "+36209598858";
                    smsController.Recipients = tmp;
                    //smsController.MessageComposeDelegate = this;
                    smsController.Finished += ((sender, args) =>
                    {
                        Console.WriteLine(args.Result.ToString());
                        (UIApplication.SharedApplication.KeyWindow.RootViewController as UINavigationController).PresentedViewController.DismissViewController(true, null);

                    });

                    (UIApplication.SharedApplication.KeyWindow.RootViewController as UINavigationController).PresentViewController(smsController, true, null);
                }
            }) { LabelFontColor = UIColor.White, Margin = new UIEdgeInsets(5, 10, 0, 10), InsideMargin = new UIEdgeInsets(10, 5, 10, 5), Height = 0 };
            var mobilControl = new ButtonControl(Helper.GetLangText("ContactMobilePhone"), () =>
            {
                if (DateTime.Today.DayOfWeek == System.DayOfWeek.Sunday ||
                    DateTime.Today.DayOfWeek == System.DayOfWeek.Saturday)
                {
                    _dialog.ShowDialogBox("Hétvége", "Sajnálom de hétvégén a telefonhívása nem lehetséges");
                }
                else
                {
                    var tel = new NSUrl("tel:" + "+36209598858");
                    UIApplication.SharedApplication.OpenUrl(tel);
                }
            }) { LabelFontColor = UIColor.White, Margin = new UIEdgeInsets(5, 10, 0, 10), InsideMargin = new UIEdgeInsets(10, 5, 10, 5), Height = 0 };
            var faxControl = new ButtonControl(Helper.GetLangText("ContactFax"), () => { }) { LabelFontColor = UIColor.White, Margin = new UIEdgeInsets(5, 10, 0, 10), InsideMargin = new UIEdgeInsets(10, 5, 10, 5), Height = 0 };
            var skypeControl = new ButtonControl(Helper.GetLangText("ContactSkype"), () =>
            {
                gpBoard.String = "Soft-Telekom";
                _dialog.ShowDialogBox("Skype", SharedTextSourceSingleton.Instance.SharedTextSource.GetText("SkypeClipboard"));
            }) { LabelFontColor = UIColor.White, Margin = new UIEdgeInsets(5, 10, 0, 10), InsideMargin = new UIEdgeInsets(10, 5, 10, 5), Height = 0 };
            var emailControl = new ButtonControl(Helper.GetLangText("ContactEmail"), Model.EmailCommand) { LabelFontColor = UIColor.White, Margin = new UIEdgeInsets(5, 10, 0, 10), InsideMargin = new UIEdgeInsets(10, 5, 10, 5), Height = 0 };

            #endregion

            #region [BaseLayout]
            Layout = new LinearLayout(Orientation.Vertical)
            {
                Layer = new CALayer(),
                LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent),
                SubViews = new View[] { }
            };
            #endregion

            #region [MainLayout]

            var mainLayout = new LinearLayout(Orientation.Vertical)
            {
                LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent)
                {
                    Gravity = Gravity.Center,
                },
                Layer = new CALayer()
                {
                    BackgroundColor = UIColor.White.CGColor
                },
                SubViews = new View[]
                {
                    new NativeView(addressControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)),  
                    new NativeView(openControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)),   
                    new NativeView(phoneControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(smsControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(mobilControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(faxControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(skypeControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(emailControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 

                
                    
                },

            };

            #endregion

            #region [BaseLayoutSettings]

            var mainScrollView = new UILayoutHostScrollable(mainLayout);
            Layout.AddSubView(mainScrollView, new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent));
            View = new UILayoutHost(Layout) { BackgroundColor = UIColor.White };

            #endregion

            NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
            NavigationController.NavigationBar.BackgroundColor = UIColor.Black;

            var set = this.CreateBindingSet<ContactView, ContactViewModel>();

            
            set.Bind(this).For(v => v.Title).To(vm => vm.TopBarTitle);
            set.Bind(NavigationController.NavigationBar).For(t => t.BarTintColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Bind(addressControl).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Bind(openControl).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Bind(phoneControl).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Bind(smsControl).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Bind(mobilControl).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Bind(faxControl).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Bind(skypeControl).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Bind(emailControl).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Apply();


        }
        
    }
}