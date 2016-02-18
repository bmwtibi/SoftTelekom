using CoreGraphics;
using SoftTelekom.iOS.Utils;
using SoftTelekom.iOS.Views.CustomViews;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views.Controls
{
    public class PickerControl
    {
        //
        public UIView MainView { get; set; }
        //public LinearLayout MainLayout { get; set; }
        public UIPickerView Picker { get; set; }
        public ButtonControl ButtonControl { get; set; }
        public bool IsEnum { get; set; }
        public PickerViewModel NotEnumModel { get; set; }
        public EnumPickerViewModel EnumModel { get; set; }
        public UILabel CloseButtonLabel { get; set; }
        public string ButtonText { get; set; }

        private UIEdgeInsets _margin = new UIEdgeInsets(0, 10, 0, 10);
        public UIEdgeInsets Margin { get { return _margin; } set { _margin = value; } }

        private UIFont _closeButtonLabelFont = Helper.DefaultFont();
        public UIFont CloseButtonLabelFont { get { return _closeButtonLabelFont; } set { _closeButtonLabelFont = value; } }

        private UIColor _closeButtonLabelFontColor = UIColor.Black;
        public UIColor CloseButtonLabelFontColor
        {
            get { return _closeButtonLabelFontColor; }
            set
            {
                _closeButtonLabelFontColor = value;
                if (CloseButtonLabel != null)
                {
                    CloseButtonLabel.TextColor = _closeButtonLabelFontColor;
                }
            }
        }

        private UIView _pickerBackgroundView;
        private UIView _pickerView;

        public PickerControl(bool isEnum)
        {
            IsEnum = isEnum;
        }

        public void Init()
        {
            _pickerBackgroundView = new UIView(UIScreen.MainScreen.Bounds) { BackgroundColor = UIColor.FromRGBA(0, 0, 0, 128), Alpha = 0.0f };
            _pickerView = new UIView(new CGRect(0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width, 256));
            Picker = new UIPickerView(new CGRect(0, 40, UIScreen.MainScreen.Bounds.Width, 216)){BackgroundColor = UIColor.White};
            if (IsEnum)
            {
                EnumModel = new EnumPickerViewModel(Picker);
            }
            else
            {
                NotEnumModel = new PickerViewModel(Picker);
            }
            var pickerCloseButtonLabel = new UILabel(new CGRect(20, 0, UIScreen.MainScreen.Bounds.Width - 40, 39))
            {
                Font = Helper.DefaultFont(),
                TextColor = UIColor.Black,
                TextAlignment = UITextAlignment.Right,
                Text = Helper.GetLangText("Done")
            };

            var pickerCloseButtonLine = new UIView(new CGRect(0, 39, UIScreen.MainScreen.Bounds.Width, 1)) { BackgroundColor = UIColor.LightGray };

            var pickerCloseButtonView = new UIView(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, 39)) { BackgroundColor = UIColor.FromRGB(255, 255, 255) };
            pickerCloseButtonView.Add(pickerCloseButtonLabel);

            pickerCloseButtonView.UserInteractionEnabled = true;
            pickerCloseButtonView.AddGestureRecognizer(new UITapGestureRecognizer(tap => ClosePicker()));

            _pickerBackgroundView.UserInteractionEnabled = true;
            _pickerBackgroundView.AddGestureRecognizer(new UITapGestureRecognizer(tap => ClosePicker()));

            _pickerView.Add(pickerCloseButtonView);
            _pickerView.Add(pickerCloseButtonLine);
            _pickerView.Add(Picker);

            ButtonControl = new ButtonControl(()=>OpenPicker()) { LabelFontColor = UIColor.Black, Margin = _margin};
            MainView = ButtonControl.GetView();
        }

        public UIView GetView()
        {
            Init();
            return MainView;
        }

        public void OpenPicker()
        {
            UIApplication.SharedApplication.KeyWindow.RootViewController.View.Add(_pickerBackgroundView);
            UIApplication.SharedApplication.KeyWindow.RootViewController.View.Add(_pickerView);
            UIView.Animate(0.3f, () =>
            {
                _pickerBackgroundView.Alpha = 1.0f;
                _pickerView.Frame = new CGRect(0, UIScreen.MainScreen.Bounds.Height - 256, UIScreen.MainScreen.Bounds.Width, 256);

            });
        }

        public void ClosePicker()
        {
            UIView.Animate(0.3f, (() =>
            {
                _pickerBackgroundView.Alpha = 0.0f;
                _pickerView.Frame = new CGRect(0, UIScreen.MainScreen.Bounds.Height, UIScreen.MainScreen.Bounds.Width, 256);
            }), () =>
            {
                _pickerView.RemoveFromSuperview();
                _pickerBackgroundView.RemoveFromSuperview();

            });
        }

    }
}