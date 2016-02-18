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
    public class BillingInfoViewCell : MvxTableViewCell, IMvxMeasurableCell<BillItem>
    {
        public static readonly NSString Key = new NSString("BillingInfoViewCell");
         public BillingInfoViewCell()
            : base(string.Empty, UITableViewCellStyle.Default, Key)
        {
        }

         public BillingInfoViewCell(IntPtr handle)
            : base(handle)
        {
        }
        public ViewGroup Layout { get; set; }
        public UILabel DateLabel { get; set; }
        public UILabel IsPaidLabel { get; set; }

        public void Initialize()
        {
            if (Layout == null)
            {
                Layout = new LinearLayout(Orientation.Horizontal)
                {
                    LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent),
                    //{
                    //    Margins = new UIEdgeInsets(0,10,0,10)
                    //},
                    SubViews = new View[]
                    {
                        new LinearLayout(Orientation.Horizontal)
                        {
                            LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent)
                            {
                                Margins = new UIEdgeInsets(0,10,0,10)
                            },
                            SubViews = new View[]
                            {
                                new NativeView()
                                {
                                    LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.FillParent),
                                    View = DateLabel = new UILabel()
                                    {
                                
                                    }
                                }, 
                                 new NativeView()
                                {
                                    LayoutParameters = new LayoutParameters(AutoSize.WrapContent,AutoSize.FillParent),
                                    View = IsPaidLabel = new UILabel()
                                    {
                                        TextAlignment = UITextAlignment.Right
                                    }
                                }, 
                            }
                        }
                    }
                };

                ContentView.Add(new UILayoutHost(Layout, ContentView.Bounds));
            }
            //  BackgroundColor = UIColor.Yellow;
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<BillingInfoViewCell, BillItem>();
                set.Bind(ContentView).For(v => v.BackgroundColor).To(vm => vm.ListBackgroundColor).WithConversion("NativeColor");
                set.Bind(DateLabel).To(vm => vm.Date).WithConversion(new DateTimeValueConverter(), "Date2");
                set.Bind(IsPaidLabel).To(vm => vm.IsPaid).WithConversion(new BoolToStringValueConverter(), "bill");
                set.Bind(IsPaidLabel).For(v=>v.TextColor).To(vm => vm.IsPaid).WithConversion(new BoolToColorValueConverter(), "bill");
                set.Apply();
            });
        }

        public void Init(BillItem item)
        {
        }

        public float MeasureHeight(UIKit.UITableView tableView, BillItem item)
        {
            return 40;
        }
    }
}