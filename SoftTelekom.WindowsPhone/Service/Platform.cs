using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Services;

namespace SoftTelekom.WindowsPhone.Service
{
    public class Platform : IPlatform
    {
        public PlatformEnum ActivePlatform()
        {
            return PlatformEnum.WP8;
        }
    }
}