using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using CoreAnimation;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.iOS.Utils;
using SoftTelekom.iOS.Views.Controls;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views
{
    public class UserInfoView : MvxViewController
    {
        protected UserInfoViewModel Model { get { return ViewModel as UserInfoViewModel; } }
        public ViewGroup Layout;
        public override void ViewDidLoad()
        {

            base.ViewDidLoad();
            #region [Control Elements]

            var nameControl = new LabelAndTextBoxControl("Név:");
            var addressControl = new LabelAndTextBoxControl("Cím:");
            var emailControl = new LabelAndTextBoxControl("E-mail:") { KeyboardType = UIKeyboardType.EmailAddress };
            var phoneControl = new LabelAndTextBoxControl("Telefonszám:") { KeyboardType = UIKeyboardType.NumberPad };
            var saveButton = new ButtonControl("Mentés", Model.SaveCommand) { LabelFontColor = UIColor.White, Margin = new UIEdgeInsets(20, 10, 0, 10) };

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
                    new NativeView(nameControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(addressControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(emailControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(phoneControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(saveButton.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                }
            };

            #endregion

            #region [BaseLayoutSettings]

            var mainScrollView = new UILayoutHostScrollable(mainLayout);
            Layout.AddSubView(mainScrollView, new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent));
            View = new UILayoutHost(Layout) { BackgroundColor = UIColor.White };

            #endregion

            NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
            NavigationController.NavigationBar.TintColor = UIColor.White;   

            #region [ Binding ]

            var set = this.CreateBindingSet<UserInfoView, UserInfoViewModel>();
            set.Bind(this).For(v => v.Title).To(vm => vm.TopBarTitle);
            set.Bind(NavigationController.NavigationBar).For(t => t.BarTintColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Bind(nameControl.InputTextField).To(vm => vm.UserInfo.Name);
            set.Bind(addressControl.InputTextField).To(vm => vm.UserInfo.Address);
            set.Bind(emailControl.InputTextField).To(vm => vm.UserInfo.Email);
            set.Bind(phoneControl.InputTextField).To(vm => vm.UserInfo.PhoneNumber);
            set.Bind(saveButton).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");

            set.Apply();

            #endregion
        }
    }
}