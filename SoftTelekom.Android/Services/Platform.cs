﻿using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Services;

namespace SoftTelekom.Android.Services
{
    public class Platform : IPlatform
    {
        public PlatformEnum ActivePlatform()
        {
            return PlatformEnum.Android;
        }
    }
}