using Cirrious.MvvmCross.Plugins.Messenger;

namespace SoftTelekom.Core.Messages
{
    public class LanguageChangeMessage : MvxMessage
    {
        public LanguageChangeMessage(object sender)
            : base(sender)
        {
        }
    }
}