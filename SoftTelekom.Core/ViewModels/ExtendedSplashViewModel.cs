using SoftTelekom.Core.Helpers;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.ViewModels.Bases;

namespace SoftTelekom.Core.ViewModels
{
    public class ExtendedSplashViewModel : MainViewModel
    {
        public ExtendedSplashViewModel(IViewModelParams param)
            : base(param)
        {
        }

        public void NavigateFirstView()
        {
            ShowViewModel<DashboardViewModel>();
        }
    }
}