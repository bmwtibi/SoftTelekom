using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using CoreAnimation;
using Foundation;
using SoftTelekom.Core.Services;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.iOS.Utils;
using SoftTelekom.iOS.Views.Controls;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views
{
    [Register("AdministrationView")]
    public class AdministrationView : MvxViewController
    {
        private IDialogService _dialog;

        public ViewGroup Layout;

        private UILabel _phoneAdministrationLabel;
        private UILabel _phoneDescriptionLabel;
        private UILabel _onlineAdministrationLabel;
        private UILabel _onlineDescriptionLabel;
        private UIButton _logInButton;

        private readonly nfloat _leftRightMargin = 10;
        protected AdministrationViewModel Model
        {
            get
            {
                return ViewModel as AdministrationViewModel;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var loginoutControl = new ButtonControl( Model.LogInCommand) { LabelFontColor = UIColor.White, Margin = new UIEdgeInsets(5, 10, 0, 10)};
            var set = this.CreateBindingSet<AdministrationView, AdministrationViewModel>();
            var userNameTextBoxControl = new TextBoxControl<AdministrationView, AdministrationViewModel>(set, vm => vm.UserName) { TitleLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("UserName"), PlaceholderText = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("UserName"), IsEnabled = vm=>vm.InputIsEnabled};
            var userPwdTextBoxControl = new TextBoxControl<AdministrationView, AdministrationViewModel>(set, vm => vm.UserPwd) { TitleLabel = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Password"), PlaceholderText = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("Password"), IsPasswordBox = true, IsEnabled = vm => vm.InputIsEnabled };
            Layout = new LinearLayout(Orientation.Vertical)
            {
                LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent),
                SubViews = new View[]
                {
                    new LinearLayout(Orientation.Horizontal)
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)
                        {
                            Margins = new UIEdgeInsets(5,_leftRightMargin,0,_leftRightMargin)
                        },
                        SubViews = new View[]
                        {
                            new NativeView()
                            {
                                LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent),
                                View = _phoneAdministrationLabel = new UILabel()
                                {
                                    TextColor = UIColor.Black,
                                    Font = UIFont.BoldSystemFontOfSize(20)
                                }
                            }, 
                        }

                    }, 
                    new LinearLayout(Orientation.Horizontal)
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)
                        {
                            Margins = new UIEdgeInsets(5,_leftRightMargin,0,_leftRightMargin)
                        },
                        SubViews = new View[]
                        {
                            new NativeView()
                            {
                                LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent),
                                View = _phoneDescriptionLabel = new UILabel()
                                {
                                    TextColor = UIColor.Black,
                                    Lines = 0
                                }
                            }, 
                        }

                    }, 
                    new LinearLayout(Orientation.Horizontal)
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)
                        {
                            Margins = new UIEdgeInsets(15,_leftRightMargin,0,_leftRightMargin)
                        },
                        SubViews = new View[]
                        {
                            new NativeView()
                            {
                                LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent),
                                View = _onlineAdministrationLabel = new UILabel()
                                {
                                    TextColor = UIColor.Black,
                                    Font = UIFont.BoldSystemFontOfSize(20)
                                }
                            }, 
                        }

                    }, 
                    new LinearLayout(Orientation.Horizontal)
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)
                        {
                            Margins = new UIEdgeInsets(5,_leftRightMargin,0,_leftRightMargin)
                        },
                        SubViews = new View[]
                        {
                            new NativeView()
                            {
                                LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent),
                                View = _onlineDescriptionLabel = new UILabel()
                                {
                                    TextColor = UIColor.Black,
                                    Lines = 0
                                }
                            }, 
                        }

                    }, 
                    new NativeView()
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent),
                        View = userNameTextBoxControl.GetLayout()
                    },                  
                    new NativeView()
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent),
                        View = userPwdTextBoxControl.GetLayout()
                    }, 
                    new NativeView(loginoutControl.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    //new LinearLayout(Orientation.Vertical) 
                    //{
                    //    LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)
                    //    {
                    //        //Gravity = Gravity.Center,
                    //        Margins = new UIEdgeInsets(20,_leftRightMargin,10,_leftRightMargin)
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
                    //            View = _logInButton = new UIButton()
                    //            {
                                    
                    //            }
                    //        }
                    //    }         
                    //},
                }
            
            };

           
            set.Bind(this).For(v => v.Title).To(vm => vm.TopBarTitle);
            set.Bind(NavigationController.NavigationBar).For(t => t.BarTintColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            _phoneAdministrationLabel.Text = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("PhoneAdministTitle");
            _phoneDescriptionLabel.Text = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("PhoneDescription");
            _onlineAdministrationLabel.Text = SharedTextSourceSingleton.Instance.SharedTextSource.GetText("OnlineAdministTitle");
            set.Bind(_onlineDescriptionLabel).To(vm => vm.OnlineDescription);

            set.Bind(loginoutControl).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");
            set.Bind(loginoutControl.Label).To(vm => vm.LogInOutButtonTitle);
            //set.Bind(_logInButton).For("Title").To(vm => vm.LogInOutButtonTitle);
            //set.Bind(_logInButton).For("Tap").To(vm => vm.LogInCommand);
            set.Apply();

            Model.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "OnlineDescription")
                {
                    _onlineAdministrationLabel.GetLayoutHost().SetNeedsLayout();
                }
            };
            View = new UILayoutHostScrollable(Layout);
            View.BackgroundColor = UIColor.White;
            View.UserInteractionEnabled = true;
            NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
            NavigationController.NavigationBar.TintColor = UIColor.White;
            View.AddGestureRecognizer(new UITapGestureRecognizer(tap =>
            {
                userNameTextBoxControl.EditField.ResignFirstResponder();
                userPwdTextBoxControl.EditField.ResignFirstResponder();
            }));
            userNameTextBoxControl.KeyboardScroll(View, (UIScrollView)View);
            userPwdTextBoxControl.KeyboardScroll(View, (UIScrollView)View);
        }
    }
}