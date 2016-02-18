using System.Drawing;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Touch.Views;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.Models;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.iOS.Converters;
using SoftTelekom.iOS.DataSources;
using SoftTelekom.iOS.Views.Cells;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views
{
    [Register("MenuView")]
    public class MenuView : MvxViewController
    {
        public MenuViewModel Model
        {
            get
            {
                return ViewModel as MenuViewModel;
            }
        }

        public ViewGroup Layout
        {
            get;
            set;
        }

        private UITableView _menuTableView;
        private UIView _menuBarView;
        private UILabel _menuTitle;
        private LinearLayout _innerLayout;
        private UIView _menuBarBg;


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (this.RespondsToSelector(new Selector("edgesForExtendedLayout")))
            {
                this.EdgesForExtendedLayout = UIRectEdge.None;
            }

            Layout = new LinearLayout(Orientation.Vertical)
            {
                Layer = new CALayer()
                {
                    BackgroundColor = UIColor.Clear.CGColor
                },
                LayoutParameters = new LayoutParameters()
                {
                    Width = AutoSize.FillParent,
                    Height = AutoSize.FillParent
                },
                SubViews = new XibFree.View[]
                {
                    new NativeView()
                    {
                        View = _menuBarBg = new UIView
                        {
                            BackgroundColor = UIColor.Black,
                            
                            },
                            LayoutParameters = new LayoutParameters()
                            {
                                Width = AutoSize.FillParent,
                                Height = 64,
                                //MarginTop = 20f
                            }
                    },

                }
            };


            if (NavigationController != null)
            {
                NavigationController.NavigationBar.BarTintColor = UIColor.Black;

                NavigationController.NavigationBar.Hidden = true;
            }

            //Title = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("LeftMenuTitle");


            _menuTableView = new UITableView()
            {
                SeparatorStyle = UITableViewCellSeparatorStyle.None,
                SeparatorColor = UIColor.Clear,
                BackgroundColor = UIColor.Clear
            };

            var source = new GenericDataSource<MenuItem, MenuItemCell>(_menuTableView);
            _menuTableView.Source = source;

            _menuTitle = new UILabel
            {
                Frame = new RectangleF(20, 20, 240, 44),
                Text = "Placeholder",
                TextAlignment = UITextAlignment.Left,
                TextColor = UIColor.White,
            };

            _menuBarView = new UIView()
            {
                BackgroundColor = UIColor.Black,
                Frame = new RectangleF(0, 0, 250, 64),
                Alpha = 1.0F
            };

            _menuBarView.AddSubview(_menuTitle);
            _menuBarBg.AddSubview(_menuBarView);

            var set = this.CreateBindingSet<MenuView, MenuViewModel>();
            set.Bind(_menuBarView).For(v => v.BackgroundColor).To(vm => vm.MenuBarColor).WithConversion("NativeColor");
            set.Bind(_menuTitle).For(v => v.Text).To(vm => vm.MenuText);
            set.Bind(_menuTitle).For(v => v.TextColor).To(vm => vm.MenuTitleColor).WithConversion("NativeColor");
            set.Bind(source).To(vm => vm.MenuItems);
            set.Bind(source).For(l => l.SelectionChangedCommand).To(vm => vm.ListItemClick);
            //set.Bind(_menuTableView).For(t => t.BackgroundColor).To(vm => vm.MenuBackgroundColor).WithConversion("NativeColor");
            //set.Bind(View).For(t => t.BackgroundColor).To(vm => vm.MenuBackgroundColor).WithConversion("NativeColor");
            set.Apply();
            Model.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "MenuItems")
                {
                    //_menuTableView.ReloadData();
                    ViewDidLoad();
                }
            };

            _innerLayout = new LinearLayout(Orientation.Vertical);

            _innerLayout.AddSubView(_menuTableView, new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent));

            Layout.AddSubView(new UILayoutHostScrollable(_innerLayout), new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent));

            Layout.Layer.Contents = null;
            Layout.Layer.Contents = UIImage.FromBundle("SoftTelekomResources/Images/MenuBg").CGImage;
            set.Bind(Layout.Layer).For(v => v.Contents).To(vm => vm.CurrenTheme).WithConversion(new MenuBgImageValueConverter());

            set.Apply();
            View = new UILayoutHost(Layout);

        }
    }
}