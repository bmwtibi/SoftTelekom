using Cirrious.MvvmCross.Plugins.Messenger;

namespace SoftTelekom.Core.Messages
{
    public class LogInOutMessage : MvxMessage
    {
        public LogInOutMessage(object sender)
            : base(sender)
        {
        }
    }
}