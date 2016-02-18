

using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.Localization;
using Cirrious.MvvmCross.ViewModels;
using SoftTelekom.Android.Services;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.ViewModels.Bases;

namespace SoftTelekom.Android
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            Mvx.LazyConstructAndRegisterSingleton<IPlatform, Platform>();
            Mvx.LazyConstructAndRegisterSingleton<IDialogService, DialogService>();
            return new Core.App();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);
            pluginManager.EnsurePluginLoaded<Cirrious.MvvmCross.Plugins.Messenger.PluginLoader>();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
            Mvx.RegisterType<IViewModelParams, MainViewModel.ViewModelParams>();
            Mvx.LazyConstructAndRegisterSingleton<INetworkService, NetworkService>();
            Mvx.LazyConstructAndRegisterSingleton<IMvxSavedStateConverter, MvxSavedStateConverter>();
        }

        protected override List<Assembly> ValueConverterAssemblies
        {
            get
            {
                List<Assembly> toReturns = base.ValueConverterAssemblies;
                toReturns.Add(typeof(MvxLanguageBinder).Assembly);
                return toReturns;
            }
        }

        //protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        //{
        //    registry.RegisterCustomBindingFactory<ImageView>("ImageSrc", view => new ImageViewDrawableBinding(view));
        //    //registry.RegisterCustomBindingFactory<MvxSpinner>("ItemSelected", spinner => new SpinnerItemSelectedBinding(spinner));
        //    registry.RegisterCustomBindingFactory<TextView>("ErrorString", text => new TextViewErrorBinding(text));
        //    registry.RegisterCustomBindingFactory<ImageView>("ImageCircle", view => new ImageCircleBinding(view));
        //    registry.RegisterCustomBindingFactory<View>("PaddingLeft", view => new ViewPaddingLeftBinding(view));
        //    base.FillTargetFactories(registry);
        //}
    }
}