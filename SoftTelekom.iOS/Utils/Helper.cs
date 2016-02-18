using System;
using SoftTelekom.Core.Utils;
using UIKit;

namespace SoftTelekom.iOS.Utils
{
    public static class Helper
    {
        public static string GetLangText(string code)
        {
            return SharedTextSourceSingleton.Instance.SharedTextSource.GetText(code);
        }


        public static UIFont DefaultFont()
        {
            return UIFont.SystemFontOfSize(15);
        }
        public static UIFont DefaultFont(nfloat size)
        {
            return UIFont.SystemFontOfSize(size);
        }
    }
}