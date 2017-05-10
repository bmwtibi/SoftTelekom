using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using Foundation;
using SoftTelekom.Core.Models;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.iOS.DataSources;
using SoftTelekom.iOS.Views.Cells;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views
{
    [Register("NewsView")]
    public class NewsView : MvxViewController
    {
        protected NewsViewModel Model
        {
            get
            {
                return ViewModel as NewsViewModel;
            }
        }
        private UIImageView _imageView;
        public ViewGroup Layout;
        private UITableView _tableView;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Layout = new LinearLayout(Orientation.Vertical)
            {
                LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.FillParent),
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
            
            View = new UILayoutHostScrollable(Layout);
            Model.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "NewsList")
                {
                    View.SetNeedsDisplay();
                    View.SetNeedsLayout();
					//_tableView.GetLayoutHost().SetNeedsLayout();
                }
            };

            var source = new GenericDataSource<NewsItem, NewsItemCell>(_tableView);
            _tableView.Source = source;
            View.BackgroundColor = UIColor.White;
           
            
            NavigationController.NavigationBar.BackgroundColor = UIColor.Black;
            var set = this.CreateBindingSet<NewsView, NewsViewModel>();
            set.Bind(this).For(v => v.Title).To(vm => vm.TopBarTitle);
            set.Bind(source).To(vm => vm.NewsList);
            set.Bind(NavigationController.NavigationBar).For(t => t.BarTintColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Apply();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            NavigationController.NavigationBar.BarStyle= UIBarStyle.BlackOpaque;
        }
    }
}