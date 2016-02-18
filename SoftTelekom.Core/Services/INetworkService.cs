using SoftTelekom.Core.Enums;

namespace SoftTelekom.Core.Services
{
    public interface INetworkService
    {
        /// <summary>
        /// Internetkapcsolat ellenőrzése
        /// </summary>
        /// <returns>A kapcsolat állapotát leíró enummal tér vissza</returns>
        NetworkEnum CheckInternet();
    }
}