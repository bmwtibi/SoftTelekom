using System;
using Android.App;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Messenger;
using SoftTelekom.Core.Services;

namespace SoftTelekom.Android.Services
{
    public class DialogService : IDialogService
    {
		private readonly Activity _activity;
		private Activity TopActivity
		{
			get
			{
				return _activity ?? Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
			}
		}


		public void ShowDialogBox(string caption, string message)
        {
			//throw new System.NotImplementedException();
			TopActivity.RunOnUiThread(() => ShowDialogBox(caption, message, null, "OK", null));
        }

		private void ShowDialogBox(string caption, string message, string negativeButtonText, string positiveButtonText, Action<bool> responseAction)
		{
			if (string.IsNullOrEmpty(message))
			{
				message = caption;
				caption = null;
			}
			var alertDialogBuilder = new AlertDialog.Builder(TopActivity);
			alertDialogBuilder.SetCancelable(false);
			if (!string.IsNullOrEmpty(caption))
			{
				alertDialogBuilder.SetTitle(caption);
			}
			if (!string.IsNullOrEmpty(message))
			{
				alertDialogBuilder.SetMessage(message);
			}
			alertDialogBuilder.SetPositiveButton(positiveButtonText,
				(sender, args) =>
				{
					((AlertDialog)sender).Hide();
					if (responseAction != null) responseAction(true);
				});
			if (!string.IsNullOrEmpty(negativeButtonText))
				alertDialogBuilder.SetNegativeButton(negativeButtonText, (sender, args) =>
				{
					((AlertDialog)sender).Hide();
					responseAction(false);
				});
			alertDialogBuilder.Show();
		}

        public void HideDialogBox()
        {
            //throw new System.NotImplementedException();
        }

        public void ShowProgressBar(string text)
        {
            //throw new System.NotImplementedException();
        }

        public void HideProgressBar()
        {
            //throw new System.NotImplementedException();
        }
    }
}