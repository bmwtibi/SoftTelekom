using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.MvvmCross.Plugins.Messenger;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.ViewModels;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SoftTelekom.Android.Views
{
    public class UserInfoView : MvxFragment<UserInfoViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //return base.OnCreateView(inflater, container, savedInstanceState);
            View ignored = base.OnCreateView(inflater, container, savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.UserInfoView, null);
            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            Mvx.Resolve<IMvxMessenger>().Publish(new MenuMessage(this, NavigationEnum.Close, DirectionEnum.Left));
        }
    }
}