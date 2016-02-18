using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Services;

namespace SoftTelekom.WindowsPhone.Service
{
    public class NetworkService : INetworkService
    {

        public Core.Enums.NetworkEnum CheckInternet()
        {
            return NetworkEnum.Wifi;
        }
    }
}