using System;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.iOS.Utils;
using SoftTelekom.iOS.Views.Controls;
using SoftTelekom.iOS.Views.CustomViews;
using UIKit;
using XibFree;
using CoreLocation;

namespace SoftTelekom.iOS.Views
{

    [Register("OrderView")]
    public class OrderView : MvxViewController
    {
        private CGRect _originalRect;

        protected OrderViewModel Model { get { return ViewModel as OrderViewModel; } }
        public ViewGroup Layout;
        private nfloat _oldPos; //teszt text mezo keyboard föle vitelére

        private UILabel _cityLabel;
        private UILabel _speedLabel;
        private UIButton _getLocationButton;

        
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            

            _originalRect = new CGRect(View.Bounds.X, View.Bounds.Y, View.Bounds.Width, View.Bounds.Height); 

            #region [Control Elements]

            var nameControl = new LabelAndTextBoxControl(Helper.GetLangText("NameRequired"), Helper.GetLangText("NameRequired"));
            var emailControl = new LabelAndTextBoxControl(Helper.GetLangText("EmailAddress"), Helper.GetLangText("EmailAddress")) { KeyboardType = UIKeyboardType.EmailAddress };
            var phoneControl = new LabelAndTextBoxControl(Helper.GetLangText("PhoneRequired"), Helper.GetLangText("PhoneRequired")) { KeyboardType = UIKeyboardType.NumberPad };
            var cityPickerControl = new PickerControl(false);
            var speedPickerControl = new PickerControl(false);
            var sendButton = new ButtonControl(Helper.GetLangText("SendOrder"), Model.SendCommand){LabelFontColor = UIColor.White , Margin = new UIEdgeInsets(20, 10, 0, 10)};

            #endregion

            #region [BaseLayout]
            Layout = new LinearLayout(Orientation.Vertical)
            {
                Layer = new CALayer(),
                LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent),
                SubViews = new View[] { }
            };
            #endregion

            #region [MainLayout]

            var mainLayout = new LinearLayout(Orientation.Vertical)
            {
                LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.FillParent)
                {
                    Gravity = Gravity.Center,
                },
                Layer = new CALayer()
                {
                  BackgroundColor  = UIColor.White.CGColor
                },
                SubViews = new View[]
                {
                    new NativeView(nameControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)),                  
                    new NativeView(emailControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)),                
                    new NativeView(phoneControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)),     
                    // new LinearLayout(Orientation.Vertical) 
                    //{
                    //    LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)
                    //    {
                    //        //Gravity = Gravity.Center,
                    //        Margins = new UIEdgeInsets(20,10,10,10)
                    //    },
                    //    Layer = new CALayer()
                    //    {
                    //        BackgroundColor = UIColor.FromRGB(167, 74, 133).CGColor
                    //    },
                    //    SubViews = new View[]
                    //    {
                    //        new NativeView()
                    //        { 
                    //            LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)
                    //            {
                    //                Margins = new UIEdgeInsets(3,10,3,10)
                    //            },
                    //            View = _getLocationButton = new UIButton()
                    //            {
                                    
                    //            }
                    //        }
                    //    }         
                    //},
                     new NativeView()
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)
                        {
                            Margins = new UIEdgeInsets(10,10,5,0)
                        },
                        View = _cityLabel = new UILabel()
                    },
                    new NativeView(cityPickerControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                     new NativeView()
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)
                        {
                            Margins = new UIEdgeInsets(10,10,5,0)
                        },
                        View = _speedLabel = new UILabel()
                    },
                    new NativeView(speedPickerControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView()
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent),
                        View = sendButton.GetView()
                    }, 
                },

            };

            #endregion

            #region [BaseLayoutSettings]

            var mainScrollView = new UILayoutHostScrollable(mainLayout);
            Layout.AddSubView(mainScrollView, new LayoutParameters(AutoSize.FillParent, AutoSize.FillParent));
            View = new UILayoutHost(Layout) { BackgroundColor = UIColor.White };

            #endregion
           
            //_getLocationButton.TitleLabel.Font = UIFont.SystemFontOfSize(12);
            //_getLocationButton.SetTitle("GPS-es szolgáltatás meghatározás",UIControlState.Normal);
            //var loactionManager = new CLLocationManager();
            //_getLocationButton.TouchUpInside += (sender, args) =>
            //{
                
            //    loactionManager.DistanceFilter = 1;
            //    loactionManager.DesiredAccuracy = CLLocation.AccuracyBest;
            //    loactionManager.Failed += (o, eventArgs) =>
            //    {
            //        Mvx.Resolve<IDialogService>().ShowDialogBox("Error", eventArgs.Error.LocalizedDescription); 
            //    };
            //    loactionManager.UpdatedLocation += (o, eventArgs) =>
            //    {
            //        if (eventArgs.NewLocation != null)
            //        {
            //            if (DataService.GetLocationServices(eventArgs.NewLocation.Coordinate.Longitude, eventArgs.NewLocation.Coordinate.Latitude))
            //            {
            //                Mvx.Resolve<IDialogService>().ShowDialogBox("Elérhető","Az ön jelenlegi pozitcióján minden szolgáltatás elérhető" );
            //                Model.SelectedCityItem = Model.CityItemList[2];
            //            }
            //            else
            //            {
            //                Mvx.Resolve<IDialogService>().ShowDialogBox("Elérhető", "Az ön jelenlegi pozitcióján nem érhető el szolgáltatás");
            //            }
            //            loactionManager.StopUpdatingLocation();
                        
            //        }
            //    };
            //    // loactionManager.loactionManager.Location.Coordinate.Latitude
            //    loactionManager.RequestWhenInUseAuthorization();
            //    loactionManager.StartUpdatingLocation();
            //    if (DataService.GetLocationServices(loactionManager.Location.Coordinate.Longitude, loactionManager.Location.Coordinate.Latitude))
            //    {
            //        Mvx.Resolve<IDialogService>().ShowDialogBox("Elérhető", "Az ön jelenlegi pozitcióján minden szolgáltatás elérhető");
            //        Model.SelectedCityItem = Model.CityItemList[2];
            //    }
            //    else
            //    {
            //        Mvx.Resolve<IDialogService>().ShowDialogBox("Elérhető", "Az ön jelenlegi pozitcióján nem érhető el szolgáltatás");
            //    }
            //    loactionManager.StopUpdatingLocation();

            //};
            View.AddGestureRecognizer(new UITapGestureRecognizer(tap =>
            {

            }));

            NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
            NavigationController.NavigationBar.TintColor = UIColor.White;
            

            #region [ Binding ]

            var set = this.CreateBindingSet<OrderView, OrderViewModel>();
            set.Bind(this).For(v => v.Title).To(vm => vm.TopBarTitle);
            set.Bind(NavigationController.NavigationBar).For(t => t.BarTintColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");

			set.Bind(nameControl.InputTextField).To(vm => vm.NameText);
			set.Bind(emailControl.InputTextField).To(vm => vm.EmailText);
			set.Bind(phoneControl.InputTextField).To(vm => vm.PhoneNumberText);

            set.Bind(cityPickerControl.ButtonControl).For(v => v.DefaultBackgroundColor).To(vm => vm.PickerColor).WithConversion("NativeColor");
            set.Bind(cityPickerControl.ButtonControl.Label).To(vm => vm.SelectedCityItem.Name);
            set.Bind(cityPickerControl.NotEnumModel).For(m => m.ItemsSourceCollectionCity).To(vm => vm.CityItemList);
            set.Bind(cityPickerControl.NotEnumModel).For(m => m.SelectedItem).To(vm => vm.SelectedCityItem);
            set.Bind(cityPickerControl.NotEnumModel).For(m => m.SelectedChangedCommand).To(vm => vm.SelectedCityItemCommand);

            set.Bind(speedPickerControl.ButtonControl).For(v => v.DefaultBackgroundColor).To(vm => vm.PickerColor).WithConversion("NativeColor");
            set.Bind(speedPickerControl.ButtonControl.Label).To(vm => vm.SelectedSpeedItem.Name);
            set.Bind(speedPickerControl.NotEnumModel).For(m => m.ItemsSourceCollectionSpeed).To(vm => vm.SpeedItemList);
            set.Bind(speedPickerControl.NotEnumModel).For(m => m.SelectedItem).To(vm => vm.SelectedSpeedItem);
            set.Bind(speedPickerControl.NotEnumModel).For(m => m.SelectedChangedCommand).To(vm => vm.SelectedSpeedItemCommand);

            set.Bind(_cityLabel).To(vm => vm.CitysLabel);
            set.Bind(_speedLabel).To(vm => vm.SpeedLabel);

            set.Bind(sendButton).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
         
            set.Apply();

            #endregion
        }
    }
}