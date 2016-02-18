using System;
using CoreAnimation;
using Foundation;
using SoftTelekom.iOS.Utils;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views.Controls
{
    public class LabelAndTextBoxControl
    {
        public UIView MainView { get; set; }
        public LinearLayout MainLayout { get; set; }
        public UILabel Label { get; set; }
        public UITextField InputTextField { get; set; }

        private UIFont _labelFont = Helper.DefaultFont();
        public UIFont LabelFont { get { return _labelFont; } set { _labelFont = value; } }

        private UIFont _textBoxFont = Helper.DefaultFont();
        public UIFont TextBoxFont { get { return _textBoxFont; } set { _textBoxFont = value; } }

        private UIFont _placeholderFont = Helper.DefaultFont();
        public UIFont PlaceholderFont { get { return _placeholderFont; } set { _placeholderFont = value; } }

        private UIKeyboardType _keyboardType = UIKeyboardType.Default;
        public UIKeyboardType KeyboardType { get { return _keyboardType; } set { _keyboardType = value; } }

        private UITextAutocapitalizationType _autocapitalizationType = UITextAutocapitalizationType.None;
        public UITextAutocapitalizationType AutocapitalizationType { get { return _autocapitalizationType; } set { _autocapitalizationType = value; } }
        public bool SecretText { get; set; }

        private bool _enable = true;
        public bool Enabled { get { return _enable; } set { _enable = value; } }
        public string LabelText { get; set; }
        public string PlaceholderText { get; set; }

        private nfloat _width = 0;
        public nfloat Width { get { return _width; } set { _width = value; } }
        public nfloat Height { get { return _labelHeight + _textBoxHeight; } }

        private nfloat _labelHeight = 35;
        public nfloat LabelHeight { get { return _labelHeight; } set { _labelHeight = value; } }

        private nfloat _textBoxHeight = 35;
        public nfloat TextBoxHeight { get { return _textBoxHeight; } set { _textBoxHeight = value; } }

        private UIEdgeInsets _labelMargin = new UIEdgeInsets(0, 10, 0, 10);
        public UIEdgeInsets LabelMargin { get { return _labelMargin; } set { _labelMargin = value; } }

        private UIEdgeInsets _textBoxMargin = new UIEdgeInsets(0, 10, 0, 10);
        public UIEdgeInsets TextBoxMargin { get { return _textBoxMargin; } set { _textBoxMargin = value; } }

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

        private UIColor _defaultLabelBackgroundColor = UIColor.Clear;
        public UIColor DefaultLabelBackgroundColor
        {
            get { return _defaultLabelBackgroundColor; }
            set { _defaultLabelBackgroundColor = value; }
        }

        private UIColor _defaultTextBoxBackgroundColor = UIColor.Clear;
        public UIColor DefaultTextBoxBackgroundColor
        {
            get { return _defaultTextBoxBackgroundColor; }
            set { _defaultTextBoxBackgroundColor = value; }
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
        private UIColor _textBoxFontColor = UIColor.Black;
        public UIColor TextBoxFontColor
        {
            get { return _textBoxFontColor; }
            set
            {
                _textBoxFontColor = value;
                if (MainLayout != null)
                {
                    InputTextField.TextColor = _textBoxFontColor;
                }
            }
        }

        private UIColor _placeholderFontColor = UIColor.LightGray;
        public UIColor PlaceholderFontColor
        {
            get { return _placeholderFontColor; }
            set
            {
                _placeholderFontColor = value;
                if (MainLayout != null)
                {
                    InputTextField.AttributedPlaceholder = new NSAttributedString(PlaceholderText, _placeholderFont, value);
                }
            }
        }


        private nfloat _borderSize = 1;
        public nfloat BorderSize { get { return _borderSize; } set { _borderSize = value; } }

        private UIColor _borderColor = UIColor.Black;
        public UIColor BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }


        public LabelAndTextBoxControl()
        {
            LabelText = "";
            PlaceholderText = "";
        }

        public LabelAndTextBoxControl(string labelText)
        {
            LabelText = labelText;
            PlaceholderText = "";
        }

        public LabelAndTextBoxControl(string labelText,string placeholderText)
        {
            LabelText = labelText;
            PlaceholderText = placeholderText;
        }

        public void Init()
        {
            MainLayout = new LinearLayout(Orientation.Vertical)
            {
                LayoutParameters = new LayoutParameters(_width == 0 ? AutoSize.FillParent : _width, Height),
                Layer = new CALayer()
                {
                    BackgroundColor = _defaultBackgroundColor.CGColor
                },
                SubViews = new View[]
                {
                    new LinearLayout(Orientation.Vertical)
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,_labelHeight)
                        {
                            Margins = _labelMargin,
                        },
                        Layer = new CALayer()
                        {
                            BackgroundColor = _defaultLabelBackgroundColor.CGColor,
                        },
                        SubViews = new View[]
                        {
                            new NativeView()
                            {
                                LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.FillParent),
                                View = Label = new UILabel()
                                {
                                    Text = LabelText,
                                    Font = _labelFont,
                                    TextColor = _labelFontColor
                                }

                            }, 
                        }
                    }, 
                    new LinearLayout(Orientation.Vertical)
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,_textBoxHeight)
                        {
                            Margins = _textBoxMargin,
                        },
                        Layer = new CALayer()
                        {
                            BackgroundColor = _defaultTextBoxBackgroundColor.CGColor,
                            BorderWidth = _borderSize,
                            BorderColor = _borderColor.CGColor
                        },
                        SubViews = new View[]
                        {
                            new NativeView()
                            {
                                LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.FillParent)
                                {
                                    Margins = new UIEdgeInsets(0,10,0,10)
                                },
                                View = InputTextField = new UITextField()
                                {
                                    AttributedPlaceholder = new NSAttributedString(PlaceholderText, _placeholderFont, _placeholderFontColor),
                                    TextColor = _textBoxFontColor,
                                    Font = _textBoxFont,
                                    AutocapitalizationType = _autocapitalizationType,
                                    KeyboardType = _keyboardType,
                                    SecureTextEntry = SecretText,
                                    AutocorrectionType = UITextAutocorrectionType.No
                                },
                                Init = v =>
                                {
                                    var textView = v.As<UITextField>();

                                    textView.ReturnKeyType = UIReturnKeyType.Done;
                                    textView.ShouldReturn = field =>
                                    {
                                        textView.ResignFirstResponder();
                                        return true;
                                    };
                                },
                            }, 
                            
                        }
                    }, 
                     
                    
                }
            };
            MainView = new UILayoutHost(MainLayout);
            MainView.UserInteractionEnabled = true;
        }

        public UIView GetView()
        {
            Init();
            return MainView;
        }
    }
}