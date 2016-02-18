using Android.Content;
using Android.Views;
using Android.Widget;

namespace SoftTelekom.Android.Utils
{
    public class CustomToast
    {
        private readonly Context _context;

        public CustomToast(Context context)
        {
            _context = context;
        }

        public Toast MakeToast(string header, string message = null)
        {
			View view = LayoutInflater.From(_context).Inflate(Resource.Layout.CustomToast, null);
			TextView titleTextView = view.FindViewById<TextView>(Resource.Id.toastTitleTextView);
			titleTextView.Text = header;
			TextView messageTextView = view.FindViewById<TextView>(Resource.Id.toastMessageTextView);
			if (string.IsNullOrEmpty(message)) messageTextView.Visibility = ViewStates.Gone;
			else messageTextView.Text = message;
			return CreateToast(view);
        }

        public Toast CreateToast(View view)
        {
            Toast toast = new Toast(_context) {Duration = ToastLength.Long, View = view};
            toast.SetGravity(GravityFlags.Top, 10, 10);
            return toast;
        }

        public Toast MakeToast(int headerId, int messageId = 0)
        {
			View view = LayoutInflater.From(_context).Inflate(Resource.Layout.CustomToast, null);
			TextView titleTextView = view.FindViewById<TextView>(Resource.Id.toastTitleTextView);
			titleTextView.SetText(headerId);
			TextView messageTextView = view.FindViewById<TextView>(Resource.Id.toastMessageTextView);
			if (messageId > 0)
			{
				messageTextView.SetText(messageId);
			}
			else
			{
				messageTextView.Visibility = ViewStates.Gone;
			}
			return CreateToast(view);
        }

        public static Toast MakeToast(Context context, string header, string message = "")
        {
            return new CustomToast(context).MakeToast(header, message);
        }

        public static Toast MakeToast(Context context, int headerId, int message = 0)
        {
            return new CustomToast(context).MakeToast(headerId, message);
        }

        public static void ShowToast(Context context, string header, string message = "")
        {
            MakeToast(context, header, message).Show();
        }

        public static void ShowToast(Context context, int header, int message = 0)
        {
            MakeToast(context, header, message).Show();
        }

        public static void ShowToast(Context context, View view)
        {
            new CustomToast(context).CreateToast(view).Show();
        }
    }
}
