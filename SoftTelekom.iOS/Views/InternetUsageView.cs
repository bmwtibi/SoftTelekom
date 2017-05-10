using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using CoreAnimation;
using Foundation;
using SoftTelekom.Core.Models;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.iOS.DataSources;
using SoftTelekom.iOS.Views.Cells;
using SoftTelekom.iOS.Views.Controls;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views
{
    [Register("InternetUsageView")]
    public class InternetUsageView : MvxViewController
    {
        protected InternetUsageViewModel Model { get { return ViewModel as InternetUsageViewModel; } }
        public ViewGroup Layout;
        private UITableView _tableView;
        public override void ViewDidLoad()
        {

            base.ViewDidLoad();
            #region [Control Elements]
            NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
            NavigationController.NavigationBar.Opaque = false;
            NavigationController.NavigationBar.TintColor = UIColor.White;
            var actualDayInfoLabel = new LabelControl("Mai napi adatforgalma");
            var actualDayLabel = new LabelControl("0 MB") { TextAlignment = UITextAlignment.Center, LabelFont = UIFont.SystemFontOfSize(26) };
            var actualInfoLabel = new LabelControl("Aktuális havi adatforgalma");
            var actualLabel = new LabelControl("0 MB"){TextAlignment = UITextAlignment.Center, LabelFont = UIFont.SystemFontOfSize(26)};
            var oldInfoLabel = new LabelControl("Régebbi havi adatforgalmak");
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
                    new NativeView(actualDayInfoLabel.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(actualDayLabel.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(actualInfoLabel.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(actualLabel.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(oldInfoLabel.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView()
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent),
                        View = _tableView = new UITableView()
                        {
                            SeparatorStyle = UITableViewCellSeparatorStyle.None
                        }
                    }, 
                }
            };

            #endregion

            #region [BaseLayoutSettings]

            var mainScrollView = new UILayoutHost(mainLayout);
            Layout.AddSubView(mainScrollView, new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent));
            View = new UILayoutHost(Layout) { BackgroundColor = UIColor.White };

            #endregion

            

            #region [ Binding ]

            var set = this.CreateBindingSet<InternetUsageView, InternetUsageViewModel>();
            var source = new GenericDataSource<InternetUsageItem, InternetUsageViewCell>(_tableView);
            _tableView.Source = source;
            set.Bind(source).To(vm => vm.InternetUsageItemList);
            set.Bind(this).For(v => v.Title).To(vm => vm.TopBarTitle);
            set.Bind(NavigationController.NavigationBar).For(t => t.BarTintColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
			set.Bind(actualDayLabel.Label).To(vm => vm.CurrentDataTodayText).WithConversion(new DataUsageToStringValueConverter());
			set.Bind(actualLabel.Label).To(vm => vm.CurrentDataText).WithConversion(new DataUsageToStringValueConverter());;
            set.Apply();

			Model.PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == "InternetUsageItemList")
				{
					View.SetNeedsDisplay();
					View.SetNeedsLayout();
					_tableView.GetLayoutHost().SetNeedsLayout();
				}
			};

            #endregion
        }
    }
}