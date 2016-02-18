using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using CoreAnimation;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.iOS.Converters;
using SoftTelekom.iOS.DataSources;
using SoftTelekom.iOS.Utils;
using SoftTelekom.iOS.Views.Controls;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views
{
    public class ReportView : MvxViewController
    {
        protected ReportViewModel Model { get { return ViewModel as ReportViewModel; } }
        public ViewGroup Layout;
        public override void ViewDidLoad()
        {

            base.ViewDidLoad();
            #region [Control Elements]
            NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
            NavigationController.NavigationBar.Opaque = false;
            NavigationController.NavigationBar.TintColor = UIColor.White;

            var problemLabel = new LabelControl("Jelenteni kívánt probléma");
            var problemPicker = new PickerControl(true);
            var sendButton = new ButtonControl(Helper.GetLangText("SendOrder"), Model.SendCommand) { LabelFontColor = UIColor.White, Margin = new UIEdgeInsets(20, 10, 0, 10) };
            
            #endregion

            #region [BaseLayout]
            Layout = new LinearLayout(Orientation.Vertical)
            {
                Layer = new CALayer(),
                LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent)
                ,
                SubViews = new View[] { }

            };
            #endregion

            #region [MainLayout]

            var mainLayout = new LinearLayout(Orientation.Vertical)
            {
                LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent)
                {
                    MarginTop = 64
                },
                Layer = new CALayer()
                {
                    BackgroundColor = UIColor.White.CGColor
                },
                SubViews = new View[]
                {
                    new NativeView(problemLabel.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(problemPicker.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new LinearLayout(Orientation.Vertical)
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,200)
                        {
                            Margins = new UIEdgeInsets(10,10,10,10)
                        },
                        Layer = new CALayer()
                        {
                            BorderColor = UIColor.Black.CGColor,
                            BorderWidth = 1,
                            MasksToBounds = true
                        },
                        SubViews = new View[]
                        {
                            new NativeView()
                            {
                                LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.FillParent)
                                {
                                    Margins = new UIEdgeInsets(2,2,2,2)
                                },
                                View = new UITextView()
                            }, 
                        }
                    },
                    new NativeView(sendButton.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    
                }
            };

            #endregion

            #region [BaseLayoutSettings]

            var mainScrollView = new UILayoutHost(mainLayout);
            Layout.AddSubView(mainScrollView, new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent));
            View = new UILayoutHost(Layout) { BackgroundColor = UIColor.White };

            #endregion



            #region [ Binding ]

            var set = this.CreateBindingSet<ReportView, ReportViewModel>();
            set.Bind(this).For(v => v.Title).To(vm => vm.TopBarTitle);
            set.Bind(NavigationController.NavigationBar).For(t => t.BarTintColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Bind(problemPicker.ButtonControl).For(v => v.DefaultBackgroundColor).To(vm => vm.PickerColor).WithConversion("NativeColor");
            set.Bind(problemPicker.ButtonControl.Label).To(vm => vm.SelectedReason).WithConversion(new EnumToStringValueConverter());
            set.Bind(problemPicker.EnumModel).For(v => v.ItemsSource).To(vm => vm.ReasonList);
            set.Bind(problemPicker.EnumModel).For(v => v.SelectedItem).To(vm => vm.SelectedReason);
            set.Bind(problemPicker.EnumModel).For(v => v.SelectedChangedCommand).To(vm => vm.SelectedReasonCommand);
            set.Bind(sendButton).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Apply();

            #endregion
        }
    }
}