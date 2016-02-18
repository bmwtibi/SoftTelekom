using System;
using Android.App;
using Android.Runtime;
using SoftTelekom.Android.Utils;

namespace SoftTelekom.Android
{
    [Application]
    public class MyApp : Application
    {
        public MyApp(IntPtr javaReferece, JniHandleOwnership transfer) : base(javaReferece, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
//#if DEBUG
            //CrashReporter.Install(ApplicationContext);
//#endif
        }
    }
}