using Cirrious.CrossCore;
using EShyMedia.MvvmCross.Plugins.Settings;
using Newtonsoft.Json;
using SoftTelekom.Core.Enums;

namespace SoftTelekom.Core.Helpers
{
    public static class Settings
    {
        /// <summary>
        /// Simply setup your settings once when it is initialized.
        /// </summary>
        private static ISettings _settings;
        private static ISettings AppSettings
        {
            get
            {
                return _settings ?? (_settings = Mvx.GetSingleton<ISettings>());
            }
        }

#region Setting Constants

        private const string LanguagesKey = "languagesKey";
        private const string ThemeKey = "themeKey";
        private const string SavedUserEmailKey = "savedUserEmailKey";
		private const string SavedUserTokenKey = "savedUserTokenKey";
		private static readonly string LanguagesDefault = JsonConvert.SerializeObject(LanguagesEnum.HU);
        private static readonly string ThemeDefault = JsonConvert.SerializeObject(ThemeEnum.Magenta);
        private static readonly string SavedUserEmailDefault = JsonConvert.SerializeObject(string.Empty);
		private static readonly string SavedUserTokenDefault = JsonConvert.SerializeObject(string.Empty);

#endregion

        public static LanguagesEnum SavedLanguages
        {
            get
            {
                return JsonConvert.DeserializeObject<LanguagesEnum>(AppSettings.GetValueOrDefault(LanguagesKey, LanguagesDefault));
            }
            set
            {
                var json = JsonConvert.SerializeObject(value);
                AppSettings.AddOrUpdateValue(LanguagesKey, json);
            }
        }
        public static ThemeEnum SavedTheme
        {
            get
            {
                return JsonConvert.DeserializeObject<ThemeEnum>(AppSettings.GetValueOrDefault(ThemeKey, ThemeDefault));
            }
            set
            {
                var json = JsonConvert.SerializeObject(value);
                AppSettings.AddOrUpdateValue(ThemeKey, json);
            }
        }

        public static string SavedUserEmail
        {
            get
            {
                return JsonConvert.DeserializeObject<string>(AppSettings.GetValueOrDefault(SavedUserEmailKey, SavedUserEmailDefault));
            }
            set
            {
                var json = JsonConvert.SerializeObject(value);
                AppSettings.AddOrUpdateValue(SavedUserEmailKey, json);
            }
        }

		public static string SavedUserToken
		{
			get
			{
				return JsonConvert.DeserializeObject<string>(AppSettings.GetValueOrDefault(SavedUserTokenKey, SavedUserTokenDefault));
			}
			set
			{
				var json = JsonConvert.SerializeObject(value);
				AppSettings.AddOrUpdateValue(SavedUserTokenKey, json);
			}
		}

    }

}