using System;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Fragging.Fragments;
using Cirrious.MvvmCross.ViewModels;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.Core.ViewModels;
using SlidingMenuSharp;

namespace SoftTelekom.Android.Views.Bases
{
    public class DashboardBaseActivity : SlidingFragment
    {
        //private WeakReference<View> _menuReference;
        private const string TAG = "DashboardBaseActivity";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetBehindContentView(Resource.Layout.Menu_Fragment);

            SlidingMenu.ShadowWidthRes = Resource.Dimension.shadow_width;
            SlidingMenu.BehindOffsetRes = Resource.Dimension.slidingmenu_offset;
            SlidingMenu.ShadowDrawableRes = Resource.Drawable.shadow;
            SlidingMenu.FadeDegree = 0.25f;
            SlidingMenu.TouchModeAbove = TouchMode.Fullscreen;

            var menuFragment = (MenuView)SupportFragmentManager.FindFragmentById(Resource.Id.slidingmenu);
            menuFragment.ViewModel = ((DashboardViewModel)ViewModel).Menu;

            //_menuReference = new WeakReference<View>(FindViewById<LinearLayout>(Resource.Id.menuImageLayout));

            //TODO muszáj a legvégén meghívni, mert csak ekkor garantált, hogy a két menü létrejött és hozzá lett adva a vezérlõhöz
            SlidingMenu.Mode = MenuMode.Left;
        }

        protected void SetActiveFragment(MvxFragment frag, IMvxViewModel model)
        {
            frag.ViewModel = model;
            var fragTx = SupportFragmentManager.BeginTransaction();
            fragTx.Replace(Resource.Id.fragmentHost, frag, frag.GetType().AssemblyQualifiedName);
            try
            {
                fragTx.CommitAllowingStateLoss();
            }
            catch (Exception ex)
            {
                Log.Error(TAG, ex.Message);
            }
        }

		//public View MenuImageView
		//{
		//	get
		//	{
		//		View target;
		//		if (!_menuReference.TryGetTarget(out target))
		//		{
		//			target = FindViewById<LinearLayout>(Resource.Id.menuImageLayout);
		//			_menuReference.SetTarget(target);
		//		}
		//		return target;
		//	}
		//}

        protected DashboardViewModel Model
        {
            get
            {
                return DataContext as DashboardViewModel;
            }
        }
    }

    public class MenuView : MvxFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
// ReSharper disable once UnusedVariable
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.menu_frame, null);
        }

        private MenuViewModel Model { get { return DataContext as MenuViewModel; } }
    }
}
