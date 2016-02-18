using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.ViewModels.Bases;
using SoftTelekom.iOS.Services;
using UIKit;

namespace SoftTelekom.iOS
{
	public class Setup : MvxTouchSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new Core.App();
		}
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
            Mvx.RegisterType<IViewModelParams, MainViewModel.ViewModelParams>();

            Mvx.LazyConstructAndRegisterSingleton<IDialogService, DialogService>();
            Mvx.LazyConstructAndRegisterSingleton<INetworkService, NetworkService>();
            Mvx.LazyConstructAndRegisterSingleton<IPlatform, Platform>();
        }
	}
}