using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels.Bases;

namespace SoftTelekom.Core.ViewModels
{
    public class WebmailViewModel : MainViewModel
    {
        public WebmailViewModel(IViewModelParams param)
            : base(param)
        {
            TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Webmail");
        } 
    }
}