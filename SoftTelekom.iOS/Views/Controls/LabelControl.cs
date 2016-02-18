using System;
using CoreAnimation;
using Foundation;
using SoftTelekom.iOS.Utils;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views.Controls
{
    public class LabelControl
    {
         public UIView MainView { get; set; }
        public LinearLayout MainLayout { get; set; }
        public UILabel Label { get; set; }

        private UIFont _labelFont = Helper.DefaultFont();
        public UIFont LabelFont { get { return _labelFont; } set { _labelFont = value; } }

        public string LabelText { get; set; }

        private nfloat _width = 0;
        public nfloat Width { get { return _width; } set { _width = value; } }

        private nfloat _labelHeight = 35;
        public nfloat LabelHeight { get { return _labelHeight; } set { _labelHeight = value; } }

        private UIEdgeInsets _labelMargin = new UIEdgeInsets(0, 10, 0, 10);
        public UIEdgeInsets LabelMargin { get { return _labelMargin; } set { _labelMargin = value; } }

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
        private UITextAlignment _textAlignment = UITextAlignment.Left;
        public UITextAlignment TextAlignment { get { return _textAlignment; } set { _textAlignment = value; } }


        public LabelControl()
        {
            LabelText = "";          
        }

        public LabelControl(string labelText)
        {
            LabelText = labelText;         
        }

        public void Init()
        {
            MainLayout = new LinearLayout(Orientation.Vertical)
            {
                LayoutParameters = new LayoutParameters(_width == 0 ? AutoSize.FillParent : _width, LabelHeight)
                {
                    Margins = _labelMargin,
                },
                Layer = new CALayer()
                {
                    BackgroundColor = _defaultBackgroundColor.CGColor
                },
                SubViews = new View[]
                {
                   
                    new NativeView()
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,LabelHeight),
                        View = Label = new UILabel()
                        {
                            Text = LabelText,
                            Font = _labelFont,
                            TextColor = _labelFontColor,
                            TextAlignment = _textAlignment
                        }

                    }, 
                        
                }, 
     
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