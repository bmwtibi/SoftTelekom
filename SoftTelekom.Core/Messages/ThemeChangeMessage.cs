using Cirrious.MvvmCross.Plugins.Messenger;

namespace SoftTelekom.Core.Messages
{
    public class ThemeChangeMessage: MvxMessage
    {
        public ThemeChangeMessage(object sender)
            : base(sender)
        {
        }
    }
}