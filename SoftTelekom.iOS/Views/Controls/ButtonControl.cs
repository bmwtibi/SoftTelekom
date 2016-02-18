using System;
using Cirrious.MvvmCross.ViewModels;
using CoreAnimation;
using SoftTelekom.iOS.Utils;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views.Controls
{
    public class ButtonControl
    {
        public UIView MainView { get; set; }
        public LinearLayout MainLayout { get; set; }
        public UILabel Label { get; set; }
        public string LabelText { get; set; }

        private UIFont _labelFont = Helper.DefaultFont();
        public UIFont LabelFont { get { return _labelFont; } set { _labelFont = value; } }

        private UIColor _defaultBackgroundColor = UIColor.Clear;
        public UIColor DefaultBackgroundColor
        {
            get { return _defaultBackgroundColor; }
            set
            {
                _defaultBackgroundColor = value;
                if (MainLayout != null)
                {
                    MainLayout.Layer.BackgroundColor = _defaultBackgroundColor.CGColor;
                }
            }
        }

        private UIColor _selectedBackgroundColor = UIColor.LightGray;
        public UIColor SelectedBackgroundColor
        {
            get { return _selectedBackgroundColor; }
            set { _selectedBackgroundColor = value; }
        }

        private UIColor _labelFontColor = UIColor.Black;
        public UIColor LabelFontColor
        {
            get { return _labelFontColor; }
            set
            {
                _labelFontColor = value;
                if (MainLayout != null)
                {
                    Label.TextColor = _labelFontColor;
                }
            }
        }

        private UIEdgeInsets _margin = new UIEdgeInsets(0, 10, 0, 10);
        public UIEdgeInsets Margin { get { return _margin; } set { _margin = value; } }

        private UIEdgeInsets _insideMargin = new UIEdgeInsets(0, 0, 0, 0);
        public UIEdgeInsets InsideMargin { get { return _insideMargin; } set { _insideMargin = value; } }

        private nfloat _width = 0;
        public nfloat Width { get { return _width; } set { _width = value; } }

        private nfloat _height = 35;
        public nfloat Height { get { return _height; } set { _height = value; } }

        public Action ExecuteAction { get; set; }
        public MvxCommand ExecuteCommand { get; set; }

        public ButtonControl(MvxCommand command)
        {
            LabelText = "";
            ExecuteCommand = command;
        }
        public ButtonControl(Action action)
        {
            LabelText = "";
            ExecuteAction = action; 
        }

        public ButtonControl(string text, MvxCommand command)
        {
            LabelText = text;
            ExecuteCommand = command;
        }
        public ButtonControl(string text, Action action)
        {
            LabelText = text;
            ExecuteAction = action;
        }

        public void Init()
        {
            MainLayout = new LinearLayout(Orientation.Vertical)
            {
                LayoutParameters = new LayoutParameters(_width == 0 ? AutoSize.FillParent : _width, _height == 0 ? AutoSize.WrapContent : _height)
                {
                    Margins = _margin
                },
                Layer = new CALayer()
                {
                    BackgroundColor = _defaultBackgroundColor.CGColor
                },
                SubViews = new View[]
                {
                    new NativeView()
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.FillParent)
                        {
                            Margins = _insideMargin
                        },
                        View = Label = new UILabel()
                        {
                            Text = LabelText,   
                            Font = _labelFont,
                            TextColor = _labelFontColor,
                            TextAlignment = UITextAlignment.Center,
                            LineBreakMode = UILineBreakMode.WordWrap,
                            Lines = 0
                        }

                    }, 
                }
            };
            MainView = new UILayoutHost(MainLayout);
            MainView.UserInteractionEnabled = true;
            MainView.AddGestureRecognizer(new CustomeTapGesture(
                ()=>MainLayout.Layer.BackgroundColor = _selectedBackgroundColor.CGColor, 
                () =>
                    {
                        MainLayout.Layer.BackgroundColor = _defaultBackgroundColor.CGColor;
                        if (ExecuteCommand != null)
                        {
                            ExecuteCommand.Execute();
                        }
                        if (ExecuteAction != null)
                        {
                            ExecuteAction.Invoke();
                        }
                    },
                ()=> MainLayout.Layer.BackgroundColor = _defaultBackgroundColor.CGColor));
        }

        public UIView GetView()
        {
            Init();
            return MainView;
        }

    }
}