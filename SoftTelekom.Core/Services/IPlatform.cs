using SoftTelekom.Core.Enums;

namespace SoftTelekom.Core.Services
{
    public interface IPlatform
    {
        /// <summary>
        /// Az éppen aktuális platform
        /// </summary>
        /// <returns></returns>
        PlatformEnum ActivePlatform();
    }
}