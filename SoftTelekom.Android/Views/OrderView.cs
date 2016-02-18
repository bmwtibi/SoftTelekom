using Android.OS;
using Android.Views;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.MvvmCross.Plugins.Messenger;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.ViewModels;

namespace SoftTelekom.Android.Views
{
    public class OrderView : MvxFragment<OrderViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //return base.OnCreateView(inflater, container, savedInstanceState);
            View ignored = base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.OrderView, null);
            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            Mvx.Resolve<IMvxMessenger>().Publish(new MenuMessage(this, NavigationEnum.Close, DirectionEnum.Left));
        }

    
    }
}