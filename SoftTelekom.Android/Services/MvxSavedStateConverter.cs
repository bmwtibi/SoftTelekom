using System;
using System.Collections.Generic;
using Android.OS;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;

namespace SoftTelekom.Android.Services
{
	public class MvxSavedStateConverter : IMvxSavedStateConverter
	{
		private const string EXTRAS_KEY = "MvxSaved";

		public IMvxBundle Read(Bundle bundle)
		{
			if (bundle == null) return null;

			var extras = bundle.GetString(EXTRAS_KEY);
			if (string.IsNullOrEmpty(extras)) return null;

			try
			{
				var converter = Mvx.Resolve<IMvxNavigationSerializer>();
				var data = converter.Serializer.DeserializeObject<Dictionary<string, string>>(extras);
				return new MvxBundle(data);
			}
			catch (Exception)
			{
				MvxTrace.Error("Problem getting the saved state - will return null - from {0}", extras);
				return null;
			}
		}

		public void Write(Bundle bundle, IMvxBundle savedState)
		{
			if (savedState == null) return;

			if (savedState.Data.Count == 0) return;

			var converter = Mvx.Resolve<IMvxNavigationSerializer>();
			var data = converter.Serializer.SerializeObject(savedState.Data);
			bundle.PutString(EXTRAS_KEY, data);
		}
	}
}