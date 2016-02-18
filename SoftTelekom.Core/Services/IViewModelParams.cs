using Cirrious.MvvmCross.Plugins.JsonLocalisation;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace SoftTelekom.Core.Services
{
    public interface IViewModelParams
    {
        IDialogService DialogService { get; }
        IMvxMessenger MessengerService { get; }
        IMvxTextProviderBuilder Builder { get; }
        INetworkService NetworkService { get; }
    }
}