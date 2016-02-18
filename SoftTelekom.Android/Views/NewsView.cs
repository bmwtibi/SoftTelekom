using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.MvvmCross.Plugins.Messenger;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.ViewModels;

namespace SoftTelekom.Android.Views
{
    public class NewsView  : MvxFragment<NewsViewModel>
    {
        //Color magenta = new Color(177, 1, 156);
        //Color blue = new Color(62, 98, 209);

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //return base.OnCreateView(inflater, container, savedInstanceState);
            View ignored = base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.NewsView, null);

            //TextView titleTextView = view.FindViewById<TextView>(Resource.Id.textview_title);

            //if (ViewModel.CurrenTheme == ThemeEnum.Magenta)
            //{
            //    titleTextView.SetTextColor(magenta);
            //}
            //else
            //{
            //    titleTextView.SetTextColor(blue);
            //}
            

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            Mvx.Resolve<IMvxMessenger>().Publish(new MenuMessage(this, NavigationEnum.Close, DirectionEnum.Left));
        }
    }
}