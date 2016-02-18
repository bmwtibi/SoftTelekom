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
    public class NewsItemCell : MvxTableViewCell, IMvxMeasurableCell<NewsItem>
    {
        public static readonly NSString Key = new NSString("NewsItemCell");

         public NewsItemCell()
            : base(string.Empty, UITableViewCellStyle.Default, Key)
        {
        }

         public NewsItemCell(IntPtr handle)
            : base(handle)
        {
        }
        public ViewGroup Layout { get; set; }

        private FrameLayout _leftLayout;
        private LinearLayout _rightLayout;
        private UIImageView _leftBgImageView;
        private UILabel _dateLabel;
        private UILabel _timeLabel;
        private UILabel _titleLabel;
        private UILabel _desctLabel;
        public void Initialize()
        {
            if (Layout == null)
            {
                Layout = new LinearLayout(Orientation.Horizontal)
                {
                    LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent),
                    SubViews = new View[]
                    {
                        _leftLayout = new FrameLayout()
                        {
                            LayoutParameters = new LayoutParameters(AutoSize.WrapContent, AutoSize.FillParent),
                            Gravity = Gravity.Center,
                            SubViews = new View[]
                            {
                                new NativeView()
                                {
                                    View = _leftBgImageView = new UIImageView(),
                                    LayoutParameters = new LayoutParameters(AutoSize.WrapContent, AutoSize.FillParent)
                                },
                                new LinearLayout(Orientation.Vertical)
                                {
                                    LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.WrapContent),
                                    Gravity = Gravity.Center,
                                    SubViews = new View[]
                                    {
                                        new NativeView()
                                        {
                                            View = _dateLabel = new UILabel()
                                            {
                                                TextAlignment = UITextAlignment.Center,
                                                Font = UIFont.SystemFontOfSize(12),
                                                TextColor = UIColor.White
                                            },
                                            LayoutParameters =
                                                new LayoutParameters(AutoSize.FillParent, AutoSize.WrapContent)
                                        },
                                        new NativeView()
                                        {
                                            View = _timeLabel = new UILabel()
                                            {
                                                TextAlignment = UITextAlignment.Center,
                                                Font = UIFont.SystemFontOfSize(12),
                                                TextColor = UIColor.White
                                            },
                                            LayoutParameters =
                                                new LayoutParameters(AutoSize.FillParent, AutoSize.WrapContent)
                                        },
                                    }
                                },
                            }
                        },
                        _rightLayout = new LinearLayout(Orientation.Vertical)
                        {
                            LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent)
                            {
                                Margins = new UIEdgeInsets(5, 15, 5, 0)
                            },
                            Gravity = Gravity.Center,
                            SubViews = new View[]
                            {
                                new NativeView()
                                {
                                    View = _titleLabel = new UILabel()
                                    {
                                        Font = UIFont.SystemFontOfSize(16),
                                        TextColor = UIColor.FromRGB(177, 1, 156) 
                                    },
                                    LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.WrapContent)
                                    {
                                        MarginBottom = 5
                                    }
                                },
                                new NativeView()
                                {
                                    View = _desctLabel = new UILabel()
                                    {
                                        Font = UIFont.SystemFontOfSize(12),
                                        Lines = 0,
                                        LineBreakMode = UILineBreakMode.WordWrap
                                    },
                                    LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.WrapContent)
                                },
                            }
                        },
                    }
                };

                ContentView.Add(new UILayoutHost(Layout, ContentView.Bounds));
            }
            //  BackgroundColor = UIColor.Yellow;
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<NewsItemCell, NewsItem>();
                set.Bind(ContentView).For(v => v.BackgroundColor).To(vm => vm.ListBackgroundColor).WithConversion("NativeColor");
                set.Bind(_leftBgImageView).For(v => v.Image).To(vm => vm.GridImage).WithConversion(new UiImageValueConverter());
                set.Bind(_dateLabel).To(vm => vm.news_time).WithConversion(new DateTimeValueConverter(),"Date");
                set.Bind(_timeLabel).To(vm => vm.news_time).WithConversion(new DateTimeValueConverter(), "Time");
                set.Bind(_titleLabel).For(v => v.TextColor).To(vm => vm.ListTextColor).WithConversion("NativeColor");
                set.Bind(_titleLabel).To(vm => vm.news_title);
                set.Bind(_desctLabel).To(vm => vm.news_descr);
                set.Apply();
            });
        }

        public void Init(NewsItem item)
        {
        }

        public float MeasureHeight(UIKit.UITableView tableView, NewsItem item)
        {
            return 80;
        }
    }
}