using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Cirrious.MvvmCross.WindowsPhone.Views;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.WindowsPhone.Helper;

namespace SoftTelekom.WindowsPhone.Views
{
    public partial class ExtendedSplashView : MvxPhonePage
    {
        private ExtendedSplashViewModel Model
        {
            get { return (ViewModel as ExtendedSplashViewModel); }
        }
        public ExtendedSplashView()
        {
            InitializeComponent();

            switch (ResolutionHelper.CurrentResolution)
            {
                case Resolutions.HD:
                    imgResolution.Source = new BitmapImage(new Uri("/Assets/Splash/1080x1920.png", UriKind.Relative));
                    break;
                case Resolutions.WXGA:
                    imgResolution.Source = new BitmapImage(new Uri("/Assets/Splash/768x1280.png", UriKind.Relative));
                    break;
                case Resolutions.WVGA:
                    imgResolution.Source = new BitmapImage(new Uri("/Assets/Splash/480x800.png", UriKind.Relative));
                    break;
                default:
                    throw new InvalidOperationException("Unknown resolution type");
            }

            Splash_Screen();
        }

        async void Splash_Screen()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            Model.NavigateFirstView();
        }
    }
}