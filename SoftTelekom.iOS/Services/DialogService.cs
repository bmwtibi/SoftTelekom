
using SoftTelekom.Core.Services;
using SoftTelekom.iOS.Views.CustomViews;
using UIKit;

namespace SoftTelekom.iOS.Services
{
    public class DialogService : IDialogService
    {
        private OwnProgressBar _ownProgressBar;
        public DialogService()
        {
            _ownProgressBar = new OwnProgressBar();
            
        }

        public void ShowDialogBox(string caption, string message)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var alert = new UIAlertView();

                if (!string.IsNullOrEmpty(caption))
                    alert.Title = caption;

                if (!string.IsNullOrEmpty(message))
                    alert.Message = message;

                alert.AddButton("Ok");
                alert.Clicked += (sender, args) =>
                {
                };

                alert.Show();
            });
        }

        public void HideDialogBox()
        {
            throw new System.NotImplementedException();
        }

        public void ShowProgressBar(string text)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                _ownProgressBar.LoadingTitle = text;
                _ownProgressBar.ShowProgressBar();
            });
            
        }

        public void HideProgressBar()
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
            _ownProgressBar.HideProgressBar();
            });
        }
    }
}