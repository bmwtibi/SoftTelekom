using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Touch.Views;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views.CustomViews
{
    class OwnProgressBar : UIViewController
    {
        private string _loadingTitle;
        public string LoadingTitle
        {
            get
            {
                return _loadingTitle;
            }
            set
            {
                _loadingTitle = value;
                _loadingLabel.Text = value;
            }
        }

        private ViewGroup _layout;

        private UILabel _loadingLabel;

        public OwnProgressBar()
        {

            //var array = new UIImage[24];
            //for (int i = 0; i < array.Length; i++)
            //{
            //    array[i] = UIImage.FromBundle("GranitResources/Images/progressbar_" + (i + 2).ToString("00"));
            //}

            _layout = new LinearLayout(Orientation.Vertical)
            {
                LayoutParameters = new LayoutParameters()
                {
                    Width = 130,
                    Height = 130,
                    Gravity = Gravity.Center,
                    // WidthUnits = Units.Absolute,
                    // HeightUnits = Units.Absolute
                },
                Layer = new CALayer()
                {
                    BackgroundColor = new UIColor(0, 0, 0, 0.7f).CGColor
                },
                SubViews = new XibFree.View[]
                {
                    //new NativeView()
                    //{
                    //    View = new UIImageView()
                    //    {
                    //        AnimationImages = array,
                    //        AnimationDuration = 1,
                    //        AnimationRepeatCount = 0
                            
                           
                    //    },
                    //    Init = view => view.As<UIImageView>().StartAnimating(),
                    //    LayoutParameters = new LayoutParameters()
                    //    {

                    //        Width = 70,
                    //        Height = 70,
                    //        Gravity = Gravity.Center,
                    //        MarginBottom = 10,
                    //        MarginTop = 10
                    //    }                   
                    //},
                    new NativeView()
                    {
                        View = _loadingLabel = new UILabel()
                        {
                            Lines = 0,
                            TextColor = UIColor.White,
                            TextAlignment = UITextAlignment.Center,
                            LineBreakMode = UILineBreakMode.WordWrap,
                            
                        },
                        LayoutParameters = new LayoutParameters()
                        {
                            Width = 110,
                            Height = 40,
                            MarginLeft = 10,
                            //MarginRight = 10,
                            MarginBottom = 10,
                            //Gravity = Gravity.Center,
                            
                        }
                    }, 
                    
          
                }
            };
        }
        public void ShowProgressBar()
        {
            _loadingLabel.Text = LoadingTitle;
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;    
            UIApplication.SharedApplication.KeyWindow.RootViewController.Add(View);
        }

        public void HideProgressBar()
        {
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
            View.RemoveFromSuperview();
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
            View.BackgroundColor = new UIColor(0, 0, 0, 0.4f);
            _loadingLabel.Text = LoadingTitle;
            UIView insideView = new UILayoutHost(_layout,
                new CGRect((UIScreen.MainScreen.Bounds.Width - 130) / 2, (UIScreen.MainScreen.Bounds.Height - 130) / 2, 130,
                    130));
            insideView.Layer.CornerRadius = 10;
            insideView.Layer.MasksToBounds = true;
            View.Add(insideView);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            _loadingLabel.Text = LoadingTitle;
        }
    }
}