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
    public class InternetUsageViewCell: MvxTableViewCell, IMvxMeasurableCell<InternetUsageItem>
    {
        public static readonly NSString Key = new NSString("InternetUsageViewCell");
         public InternetUsageViewCell()
            : base(string.Empty, UITableViewCellStyle.Default, Key)
        {
        }

         public InternetUsageViewCell(IntPtr handle)
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
                var set = this.CreateBindingSet<InternetUsageViewCell, InternetUsageItem>();
                set.Bind(ContentView).For(v => v.BackgroundColor).To(vm => vm.ListBackgroundColor).WithConversion("NativeColor");
                set.Bind(DateLabel).To(vm => vm.Date).WithConversion(new DateTimeValueConverter(), "Date2");
                set.Bind(IsPaidLabel).To(vm => vm.DataUsage).WithConversion(new DataUsageToStringValueConverter());
                set.Apply();
            });
        }

        public void Init(InternetUsageItem item)
        {
        }

        public float MeasureHeight(UIKit.UITableView tableView, InternetUsageItem item)
        {
            return 40;
        }
    }
}