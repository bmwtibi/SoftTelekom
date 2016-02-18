using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using SoftTelekom.Core.Models;
using SoftTelekom.iOS.Converters;
using SoftTelekom.iOS.Views.Cells.Base;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views.Cells
{
    public class MenuItemCell : MvxTableViewCell, IMvxMeasurableCell<MenuItem>
    {
        public static readonly NSString Key = new NSString("MenuItemCell");

        public MenuItemCell()
            : base(string.Empty, UITableViewCellStyle.Default, Key)
        {
        }

        public MenuItemCell(IntPtr handle)
            : base(handle)
        {
        }

        public ViewGroup Layout { get; set; }

        private UILabel _menuTitle;
        private UIImageView _menuImage;

        public void Initialize()
        {
            if (Layout == null)
            {
                Layout = new LinearLayout(Orientation.Horizontal)
                {
                    LayoutParameters = new LayoutParameters()
                    {
                        Width = AutoSize.FillParent,
                        Height = AutoSize.FillParent,
                        Gravity = Gravity.Center,

                    },
                    SubViews = new XibFree.View[]
                    {
                        new NativeView()
                        {
                            View = _menuImage = new UIImageView()
                            {
                                
                            },
                            LayoutParameters = new LayoutParameters()
                            {
                                Width = 26,
                                Height = 26,
                                Gravity = Gravity.Center,
                                MarginLeft = 20f,
                            }
                        }, 
                        new NativeView()
                        {
                            View = _menuTitle = new UILabel()
                            {
                                //Font = Utils.Helpers.OpenSansCondensedLight(16f),
                                TextColor = UIColor.White
                            },
                            LayoutParameters = new LayoutParameters()
                            {
                                Width = AutoSize.WrapContent,
                                Height = AutoSize.WrapContent,
                                Gravity = Gravity.Center,
                                MarginLeft = 20f
                            }
                        }, 
                    },

                };
            }

            this.SelectionStyle = UITableViewCellSelectionStyle.None;

            BackgroundColor = UIColor.Clear;
            _menuTitle.TextColor = UIColor.FromRGB(255, 255, 255);//(226, 201, 223);
            this.ContentView.Add(new UILayoutHost(Layout, this.ContentView.Bounds));
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<MenuItemCell, MenuItem>();

                set.Bind(_menuTitle).To(vm => vm.Title);
                set.Bind(_menuImage).For(i => i.Image).To(vm => vm.MenuIndex).WithConversion(new MenuIndexToImageValueConverter(), "LeftMenuImage");
                set.Apply();
            });
        }

        public void Init(MenuItem item)
        {
            //menuTitle.Text = "[MenuName]";

        }

        public float MeasureHeight(UITableView tableView, MenuItem item)
        {
            // Initialize the view's so they have the correct content for height calculations
            //Init(item);

            // Remeasure the layout using the tableView width, allowing for grouped table view margins
            // and the disclosure indicator
            //Layout.Measure(tableView.Bounds.Width - 20 - 18, float.MaxValue);

            // Grab the measured height
            //return Layout.GetMeasuredSize().Height > 0 ? Layout.GetMeasuredSize().Height : 88;
            //fixx méret
            return 60;
        }

        public override void SetSelected(bool selected, bool animated)
        {
            //base.SetSelected(selected, animated);
        }
    }
}