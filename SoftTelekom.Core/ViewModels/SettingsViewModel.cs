using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Cirrious.CrossCore;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Helpers;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.Models;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels.Bases;

namespace SoftTelekom.Core.ViewModels
{
    public class SettingsViewModel : MainViewModel
    {
        private MvxSubscriptionToken _tokenLang;
        public SettingsViewModel(IViewModelParams param)
            : base(param)
        {    
            InitText();
            _tokenLang = Mvx.Resolve<IMvxMessenger>().Subscribe<LanguageChangeMessage>(message =>
            {
                param.Builder.LoadResources(Settings.SavedLanguages == LanguagesEnum.HU ? "Hungarian" : "English");
                InitText();
            });

        }

        public void InitText()
        {
            SaveButtonLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Save");
            TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Settings");
            SelectedLanguage = Settings.SavedLanguages;
            SelectedTheme = Settings.SavedTheme;
            ThemeList = new Collection<ThemeEnum>(Enum.GetValues(typeof(ThemeEnum)).Cast<ThemeEnum>().ToList());
        }

        public MvxCommand SaveCommand
        {
            get
            {
                return new MvxCommand(SaveExecute);
            }
        }
        private void SaveExecute()
        {
            if (Settings.SavedLanguages != SelectedLanguage)
            {
                Settings.SavedLanguages = SelectedLanguage;
                MessengerService.Publish(new LanguageChangeMessage(this));
                
            }
            if (Settings.SavedTheme != SelectedTheme)
            {
                Settings.SavedTheme = SelectedTheme;
                MessengerService.Publish(new ThemeChangeMessage(this));
                
            }
            MessengerService.Publish(new MenuItemSelectedMessage(this, typeof(DashboardViewModel), 0));
        }


        private Collection<ThemeEnum> _themeList;
        public Collection<ThemeEnum> ThemeList
        {
            get { return _themeList; }
            set { _themeList = value; RaisePropertyChanged(() => ThemeList); }
        }

        //public object ThemeList
        //{
        //    get
        //    {
        //        return from e in Enum.GetValues(typeof(ThemeEnum)).Cast<ThemeEnum>()
        //               select e;
        //    }
        //}
        private int _selectedThemeIndex;
        public int SelectedThemeIndex
        {
            get { return _selectedThemeIndex; }
            set 
            { 
                _selectedThemeIndex = value;
                SelectedTheme = ThemeList.ElementAt(value);
                RaisePropertyChanged(() => SelectedThemeIndex); 
            }
        }

        private ThemeEnum _selectedTheme;
        public ThemeEnum SelectedTheme
        {
            get { return _selectedTheme; }
            set { _selectedTheme = value; RaisePropertyChanged(() => SelectedTheme); }
        }

        public MvxCommand<ThemeEnum> SelectedThemeCommand
        {
            get
            {
                return new MvxCommand<ThemeEnum>(SelectedThemeExecute);
            }
        }
        private void SelectedThemeExecute(ThemeEnum item)
        {
            SelectedTheme = item;
        }


        public object LanguagesList
        {
            get
            {
                return from e in Enum.GetValues(typeof(LanguagesEnum)).Cast<LanguagesEnum>()
                       select e;
            }
        }

        private int _selectedLanguageIndex;
        public int SelectedLanguageIndex
        {
            get { return _selectedLanguageIndex; }
            set
            {
                _selectedLanguageIndex = value;
                SelectedLanguage = value == 0 ? LanguagesEnum.HU : LanguagesEnum.EN;
                RaisePropertyChanged(() => SelectedLanguageIndex);
            }
        }

        private LanguagesEnum _selectedLanguage;
        public LanguagesEnum SelectedLanguage
        {
            get { return _selectedLanguage; }
            set { _selectedLanguage = value; RaisePropertyChanged(() => SelectedLanguage); }
        }

        public MvxCommand<LanguagesEnum> SelectedLanguageCommand
        {
            get
            {
                return new MvxCommand<LanguagesEnum>(SelectedLanguageExecute);
            }
        }
        private void SelectedLanguageExecute(LanguagesEnum item)
        {
            SelectedLanguage = item;
        }

        private string _saveButtonLabel;
        public string SaveButtonLabel
        {
            get { return _saveButtonLabel; }
            set { _saveButtonLabel = value; RaisePropertyChanged(() => SaveButtonLabel); }
        }

    }
}