using Cirrious.MvvmCross.Plugins.Messenger;
using SoftTelekom.Core.Enums;

namespace SoftTelekom.Core.Messages
{
    public class MenuMessage : MvxMessage
    {
        public NavigationEnum Navigation { get; set; }
        public DirectionEnum Direction { get; set; }
        public MenuMessage(object sender, NavigationEnum navigation, DirectionEnum direction)
            : base(sender)
        {
            Navigation = navigation;
            Direction = direction;
        }
    }
}