using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using CoreAnimation;
using SoftTelekom.Core.Models;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.iOS.DataSources;
using SoftTelekom.iOS.Views.Cells;
using SoftTelekom.iOS.Views.Controls;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views
{
    public class BillingInfoView : MvxViewController
    {
        protected BillingInfoViewModel Model { get { return ViewModel as BillingInfoViewModel; } }
        public ViewGroup Layout;
        private UITableView _tableView;
        public override void ViewDidLoad()
        {

            base.ViewDidLoad();
            #region [Control Elements]

            #endregion

            #region [BaseLayout]
            Layout = new LinearLayout(Orientation.Vertical)
            {
                Layer = new CALayer(),
                LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent),
                SubViews = new View[] {}
                
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
                    new NativeView()
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.FillParent),
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

            NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
            NavigationController.NavigationBar.TintColor = UIColor.White;

            #region [ Binding ]

            var set = this.CreateBindingSet<BillingInfoView, BillingInfoViewModel>();
            var source = new GenericDataSource<BillItem, BillingInfoViewCell>(_tableView);
            _tableView.Source = source;
            set.Bind(source).To(vm => vm.BillItemList);
            set.Bind(this).For(v => v.Title).To(vm => vm.TopBarTitle);
            set.Bind(NavigationController.NavigationBar).For(t => t.BarTintColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");

            set.Apply();

            #endregion
        }
    }
}