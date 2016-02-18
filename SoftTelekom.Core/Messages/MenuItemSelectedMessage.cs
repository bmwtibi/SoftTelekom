using System;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace SoftTelekom.Core.Messages
{
    public class MenuItemSelectedMessage : MvxMessage
    {
        public Type ViewModelType { get; set; }
        public int MenuIndex { get; set; }

        public MenuItemSelectedMessage(object sender, Type viewModelType, int menuIndex)
            : base(sender)
        {
            ViewModelType = viewModelType;
            MenuIndex = menuIndex;
        }
    }
}