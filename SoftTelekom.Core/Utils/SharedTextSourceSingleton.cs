using Cirrious.MvvmCross.Localization;

namespace SoftTelekom.Core.Utils
{
    public class SharedTextSourceSingleton
    {
        private static SharedTextSourceSingleton _instance;

        public static SharedTextSourceSingleton Instance
        {
            get { return _instance ?? (_instance = new SharedTextSourceSingleton()); }
        }

        public IMvxLanguageBinder SharedTextSource;

        public SharedTextSourceSingleton()
        {
            SharedTextSource = new MvxLanguageBinder(Constants.GeneralNamespace, Constants.Shared);
        }
    }
}