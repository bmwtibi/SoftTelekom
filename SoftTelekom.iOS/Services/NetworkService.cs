using Cirrious.MvvmCross.Plugins.Network.Reachability;
using Cirrious.MvvmCross.Plugins.Network.Touch;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Services;

namespace SoftTelekom.iOS.Services
{
    public class NetworkService : INetworkService
    {
        public NetworkEnum CheckInternet()
        {
            switch (MvxReachability.InternetConnectionStatus())
            {
                case MvxReachabilityStatus.ViaWiFiNetwork:
                    {
                        return NetworkEnum.Wifi;
                    }
                case MvxReachabilityStatus.ViaCarrierDataNetwork:
                    {
                        return NetworkEnum.Mobile;
                    }
                default:
                    {
                        return NetworkEnum.None;
                    }
            }
        }
    }
}