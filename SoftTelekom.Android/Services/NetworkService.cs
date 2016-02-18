using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Services;

namespace SoftTelekom.Android.Services
{
    public class NetworkService : INetworkService
    {

        public NetworkEnum CheckInternet()
        {
            return NetworkEnum.Wifi;
        }
    }
}