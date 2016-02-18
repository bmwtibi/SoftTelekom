using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Services;

namespace SoftTelekom.iOS.Services
{
    public class Platform : IPlatform
    {
        public PlatformEnum ActivePlatform()
        {
            return PlatformEnum.iOS;
        }
    }
}