using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using Foundation;
using SoftTelekom.Core.ViewModels;
using UIKit;

namespace SoftTelekom.iOS.Views
{
    public class WebmailView : MvxViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad(); 
            NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
            NavigationController.NavigationBar.Opaque = false;
            NavigationController.NavigationBar.TintColor = UIColor.White;
            var webView = new UIWebView(View.Bounds);
            Add(webView);
            string url = "http://mail.soft-telekom.hu/";
            webView.LoadRequest(new NSUrlRequest(new NSUrl(url)));
            var set = this.CreateBindingSet<WebmailView, WebmailViewModel>();
            set.Bind(this).For(v => v.Title).To(vm => vm.TopBarTitle);
            set.Bind(NavigationController.NavigationBar).For(t => t.BarTintColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");

            set.Apply();
        }
    }
}