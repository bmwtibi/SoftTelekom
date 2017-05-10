using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Cirrious.CrossCore;
using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Plugins.Messenger;
using Newtonsoft.Json;
using SoftTelekom.Core.Enums;
using SoftTelekom.Core.Helpers;
using SoftTelekom.Core.Messages;
using SoftTelekom.Core.Models;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels.Bases;

namespace SoftTelekom.Core.ViewModels
{
    public class NewsViewModel :MainViewModel
    {
        private MvxSubscriptionToken _tokenLang;
        private MvxSubscriptionToken _tokenTheme;
        public NewsViewModel(IViewModelParams param) : base(param)
        {
			//_tokenLang = Mvx.Resolve<IMvxMessenger>().Subscribe<LanguageChangeMessage>(message =>
			//{
			//    param.Builder.LoadResources(Settings.SavedLanguages == LanguagesEnum.HU ? "Hungarian" : "English");
			//    TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("News");
			//    DataService.GetNews(SharedTextSourceSingleton.Instance.SharedTextSource.GetText("NewsUrl"), list =>
			//    {
			//        NewsList = new ObservableCollection<NewsItem>(list);
			//    },
			//error => Mvx.Resolve<IDialogService>().ShowDialogBox("Hiba", error.Message));
			//});
			//TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("News");
			//NewsList = new ObservableCollection<NewsItem>();

			LoadData();
            _tokenTheme = param.MessengerService.Subscribe<ThemeChangeMessage>(message => { NewsList = NewsList; });
            _tokenLang = Mvx.Resolve<IMvxMessenger>().Subscribe<LanguageChangeMessage>(message =>
            {
                param.Builder.LoadResources(Settings.SavedLanguages == LanguagesEnum.HU ? "Hungarian" : "English");
                TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("News");
    //            NewsList = Settings.SavedLanguages == LanguagesEnum.EN ? new ObservableCollection<NewsItem>(new List<NewsItem>()
    //        {
                
    //        }) : new ObservableCollection<NewsItem>(new List<NewsItem>()
    //        {
				//new NewsItem()
				//{
				//	news_time = new DateTime(2016,1,8,10,00,00),
				//	news_title = "Információ",
				//	news_descr = "Az időjárás miatt előfordulhatnak kisebb szolgáltatás kimaradások"
				//},
    //            new NewsItem()
    //            {
    //                news_time = new DateTime(2016,12,24,9,00,00),
    //                news_title = "Aktuális",
    //                news_descr = "2017. január 1-től az internet csomagok ára 18%-al csökken"
    //            }
    //        }); 
            });
            TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("News");
    //        NewsList = new ObservableCollection<NewsItem>(new List<NewsItem>()
    //        {
    //            new NewsItem()
				//{
				//	news_time = new DateTime(2016,1,8,10,00,00),
				//	news_title = "Információ",
				//	news_descr = "Az időjárás miatt előfordulhatnak kisebb szolgáltatás kimaradások"
				//},
				//new NewsItem()
				//{
				//	news_time = new DateTime(2016,12,24,9,00,00),
				//	news_title = "Aktuális",
				//	news_descr = "2017. január 1-től az internet csomagok ára 18%-al csökken"
				//}
    //        });
			//DataService.GetStatus();
            //string json = JsonConvert.SerializeObject(NewList);
            //Debug.WriteLine(json);
        }

		public void LoadData() 
		{ 
			InvokeOnMainThread(() =>
			{
				DataServiceSingletone.Instance.Service.GetNews(list =>
			{
				InvokeOnMainThread(() =>
				{
					NewsList = new ObservableCollection<NewsItem>(list);
				});

			},
			error => Mvx.Resolve<IDialogService>().ShowDialogBox("Hiba", error));
			});
		}

        private ObservableCollection<NewsItem> _newsList;
        public ObservableCollection<NewsItem> NewsList
        {
            get { return _newsList; }
            set {   
                    _newsList = value;
				if (_newsList != null)
				{
					for (int i = 0; i < _newsList.Count; i++)
					{
						_newsList[i].ListTextColor = CurrenTheme == ThemeEnum.Magenta ? new MvxColor(177, 1, 156) : new MvxColor(62, 98, 209);
						if (i % 2 == 0)
						{
							_newsList[i].ListBackgroundColor = new MvxColor(233, 237, 238);
							_newsList[i].GridImage = CurrenTheme == ThemeEnum.Magenta ? "MagentaLight" : "BlueLight";
						}
						else
						{
							_newsList[i].ListBackgroundColor = new MvxColor(242, 245, 245);
							_newsList[i].GridImage = CurrenTheme == ThemeEnum.Magenta ? "MagentaDark" : "BlueDark";
						}
					}
				}
                    
                    RaisePropertyChanged(() => NewsList); 
                }
        }
         
    }
}