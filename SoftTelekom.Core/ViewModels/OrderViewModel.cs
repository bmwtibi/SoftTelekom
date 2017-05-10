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
    public class OrderViewModel : MainViewModel
    {
        private readonly IDialogService _dialog;

        private MvxSubscriptionToken _tokenLang;

        public OrderViewModel(IViewModelParams param)
            : base(param)
        {
            _dialog = param.DialogService;            
            InitText();
            InitCityItemList();
            InitSpeedItemList();
            _tokenLang = Mvx.Resolve<IMvxMessenger>().Subscribe<LanguageChangeMessage>(message =>
            {
                param.Builder.LoadResources(Settings.SavedLanguages == LanguagesEnum.HU ? "Hungarian" : "English");
                InitText();
                InitCityItemList();
                InitSpeedItemList();
            });
        }

        #region [ Private methods ]

        private void InitText()
        {
            TopBarTitle = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Order");
            NameLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("NameRequired");
            PhoneNumberLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("PhoneRequired");
            EmailLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("EmailAddress");
            CitysLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("CityRequired");
            SpeedLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("SelectedSpeedRequired");
            SendButtonLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("SendOrder");
        }

        private void InitCityItemList()
        {
            
            CityItemList = new ObservableCollection<CityItem>()
            {
                new CityItem()
                {
                    Name = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("PlsSelect")
                },
                new CityItem()
                {
                    Name = "Barabás"
                },
                new CityItem()
                {
                    Name = "Eperjeske"
                },
                new CityItem()
                {
                    Name = "Fényeslitke"
                },
                new CityItem()
                {
                    Name = "Gelénes"
                },
                new CityItem()
                {
                    Name = "Komoró"
                },
                new CityItem()
                {
                    Name = "Lónya"
                },
                new CityItem()
                {
                    Name = "Mátyus"
                },
                new CityItem()
                {
                    Name = "Mándok"
                },
                new CityItem()
                {
                    Name = "Révleányvár"
                },
                new CityItem()
                {
                    Name = "Tiszaadony"
                },
                new CityItem()
                {
                    Name = "Tiszabezdéd"
                },
                new CityItem()
                {
                    Name = "Tiszakerecseny"
                },
                new CityItem()
                {
                    Name = "Tiszakóród"
                },
                new CityItem()
                {
                    Name = "Tiszamogyorós"
                },
                new CityItem()
                {
                    Name = "Tiszaszalka"
                },
                new CityItem()
                {
                    Name = "Tiszaszentmárton"
                },
                new CityItem()
                {
                    Name = "Tiszavid"
                },
                new CityItem()
                {
                    Name = "Tuzsér"
                },
                new CityItem()
                {
                    Name = "Zemplénagárd"
                },
                new CityItem()
                {
                    Name = "Zsurk"
                },
                new CityItem()
                {
                    Name = "Vámosatya"
                },
                new CityItem()
                {
                    Name = "Záhony"
                },
            };
            SelectedCityItem = CityItemList.First();
        }

        private void InitSpeedItemList()
        {
            SpeedItemList = new ObservableCollection<SpeedItem>()
            {
                new SpeedItem()
                {
                    Name = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("PlsSelect")
                },
                new SpeedItem()
                {
                    Name = "1 Mbit/s",
                    Mbit = 1
                },
                new SpeedItem()
                {
                    Name = "2 Mbit/s",
                    Mbit = 2
                },
                new SpeedItem()
                {
                    Name = "3 Mbit/s",
                    Mbit = 3
                },
                new SpeedItem()
                {
                    Name = "4 Mbit/s",
                    Mbit = 4
                },
            };
            SelectedSpeedItem = SpeedItemList.First();
        }

        #endregion [ Private methods ]

        #region [ Properties ]
        private string _nameLabel;
        public string NameLabel
        {
            get { return _nameLabel; }
            set { _nameLabel = value; RaisePropertyChanged(() => NameLabel); }
        }

        private string _nameText;
        public string NameText
        {
            get { return _nameText; }
            set { _nameText = value; RaisePropertyChanged(() => NameText); }
        }

        private string _emailLabel;
        public string EmailLabel
        {
            get { return _emailLabel; }
            set { _emailLabel = value; RaisePropertyChanged(() => EmailLabel); }
        }

        private string _emailText;
        public string EmailText
        {
            get { return _emailText; }
            set { _emailText = value; RaisePropertyChanged(() => EmailText); }
        }

        private string _phoneNumberLabel;
        public string PhoneNumberLabel
        {
            get { return _phoneNumberLabel; }
            set { _phoneNumberLabel = value; RaisePropertyChanged(() => PhoneNumberLabel); }
        }

        private string _phoneNumberText;
        public string PhoneNumberText
        {
            get { return _phoneNumberText; }
            set { _phoneNumberText = value; RaisePropertyChanged(() => PhoneNumberText); }
        }
        private string _citysLabel;
        public string CitysLabel
        {
            get { return _citysLabel; }
            set { _citysLabel = value; RaisePropertyChanged(() => CitysLabel); }
        }
        private string _speedLabel;
        public string SpeedLabel
        {
            get { return _speedLabel; }
            set { _speedLabel = value; RaisePropertyChanged(() => SpeedLabel); }
        }
        private string _sendButtonLabel;
        public string SendButtonLabel
        {
            get { return _sendButtonLabel; }
            set { _sendButtonLabel = value; RaisePropertyChanged(() => SendButtonLabel); }
        }

        private int _selectedCityIndex;
        public int SelectedCityIndex
        {
            get { return _selectedCityIndex; }
            set
            {
                _selectedCityIndex = value;
                SelectedCityItem = CityItemList.ElementAt(value);
                RaisePropertyChanged(() => SelectedCityIndex);
            }
        }

        private int _selectedSpeedIndex;
        public int SelectedSpeedIndex
        {
            get { return _selectedSpeedIndex; }
            set
            {
                _selectedSpeedIndex = value;
                SelectedSpeedItem = SpeedItemList.ElementAt(value);
                RaisePropertyChanged(() => SelectedSpeedIndex);
            }
        }

        private CityItem _selectedCityItem;
        public CityItem SelectedCityItem
        {
            get { return _selectedCityItem; }
            set { _selectedCityItem = value; RaisePropertyChanged(() => SelectedCityItem); }
        }

        private SpeedItem _selectedSpeedItem;
        public SpeedItem SelectedSpeedItem
        {
            get { return _selectedSpeedItem; }
            set { _selectedSpeedItem = value; RaisePropertyChanged(() => SelectedSpeedItem); }
        }

        private ObservableCollection<CityItem> _cityItemList;
        public ObservableCollection<CityItem> CityItemList
        {
            get { return _cityItemList; }
            set { _cityItemList = value; RaisePropertyChanged(() => CityItemList); }
        }
        
        private ObservableCollection<SpeedItem> _speedItemList;
        public ObservableCollection<SpeedItem> SpeedItemList
        {
            get { return _speedItemList; }
            set { _speedItemList = value; RaisePropertyChanged(() => SpeedItemList); }
        }

        #endregion [ Properties ]

        #region [ Commands ]
        public  MvxCommand SendCommand
        {
            get
            {
                return new MvxCommand(SendExecute);
            }
        }
        private void SendExecute()
        {
            if (string.IsNullOrWhiteSpace(_nameText) || string.IsNullOrWhiteSpace(_phoneNumberText) || SelectedCityItem.Name == SharedTextSourceSingleton.Instance.SharedTextSource.GetText("PlsSelect") || SelectedSpeedItem.Name == SharedTextSourceSingleton.Instance.SharedTextSource.GetText("PlsSelect"))
            {
                _dialog.ShowDialogBox(SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Error"), SharedTextSourceSingleton.Instance.SharedTextSource.GetText("ErrorEmptyFieldRemaning"));
            }
            else
            {
				var orderModel = new OrderModel()
				{
					Name = NameText,
					Email = EmailText,
					PhoneNumber = PhoneNumberText,
					Location = SelectedCityItem.Name,
					Speed = SelectedSpeedItem.Mbit
				};
				DataServiceSingletone.Instance.Service.PostOrder(orderModel,(bool isSucces, string errorMessage, bool isError) => 
				{
					if (isSucces && !isError)
					{
						InvokeOnMainThread(() =>
						{ 
							_dialog.ShowDialogBox(SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Ordered"), SharedTextSourceSingleton.Instance.SharedTextSource.GetText("MessageSuccessOrder")); 
							NameText = "";
							EmailText = "";
							PhoneNumberText = "";
							SelectedCityItem = CityItemList.First();
							SelectedSpeedItem = SpeedItemList.First();
						});
					}
					else 
					{ 
						_dialog.ShowDialogBox(SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Ordered"), "Sikertelen Rendelés");
					}
				});
                
                InvokeOnMainThread(() =>
                {
                    
                });
                
            }
        }
        public MvxCommand<CityItem> SelectedCityItemCommand
        {
            get
            {
                return new MvxCommand<CityItem>(SelectedCityItemExecute);
            }
        }
        private void SelectedCityItemExecute(CityItem item)
        {
            SelectedCityItem = item;
        }
        public MvxCommand<SpeedItem> SelectedSpeedItemCommand
        {
            get
            {
                return new MvxCommand<SpeedItem>(SelectedSpeedItemExecute);
            }
        }
        private void SelectedSpeedItemExecute(SpeedItem item)
        {
            SelectedSpeedItem = item;
        }
        #endregion [ Commands ]

    }
}