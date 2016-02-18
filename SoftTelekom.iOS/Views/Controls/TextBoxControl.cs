using System;
using System.Linq.Expressions;
using Cirrious.MvvmCross.Binding.BindingContext;
using CoreAnimation;
using CoreGraphics;
using SoftTelekom.iOS.Converters;
using UIKit;
using XibFree;
namespace SoftTelekom.iOS.Views.Controls
{
    public class TextBoxControl<TOwningTarget, TSource> where TOwningTarget : class, IMvxBindingContextOwner
    {

        #region public properties

        public MvxFluentBindingDescriptionSet<TOwningTarget, TSource> Binding { get; set; }

        /// <summary>
        /// A gomb View-ja
        /// </summary>
        public UIView Layout { get; set; }

        /// <summary>
        /// Bevitelimező
        /// </summary>
        public UITextField EditField { get; set; }

        /// <summary>
        /// Bevietli mező feletti szöveg szöveges formába megadás esetén
        /// </summary>
        public string TitleLabel { get; set; }

        /// <summary>
        /// Beviteli mező örökké inaktívra állításához
        /// </summary>
        public bool IsEnabledWithoutBind { get; set; }

        /// <summary>
        /// A bevietli mező billentyűzetének típusa
        /// </summary>
        public UIKeyboardType KeyboardType { get; set; }

        /// <summary>
        /// Szöbeg igazitása
        /// </summary>
        public UITextAlignment TextFieldAlign { get; set; }

        /// <summary>
        /// Bevieteli mező jalszó tipusú-e
        /// </summary>
        public bool IsPasswordBox { get; set; }

        /// <summary>
        /// Bevihető szöveg hossza
        /// </summary>
        public int TextLength { get; set; }

        /// <summary>
        /// Az a görgethető View amin a Control elhelyezkedik
        /// </summary>
        public UIScrollView ScrollView { get; set; }

        /// <summary>
        /// A gögethető View eredeti helyzete
        /// </summary>
        public nfloat OldScrollPos { get; set; }

        /// <summary>
        /// Beviteli mező Placeholderje
        /// </summary>
        public string PlaceholderText { get; set; }

        /// <summary>
        /// TextPlaceholder with bind
        /// </summary>
        public Expression<Func<TSource, object>> PlaceholderBind { get; set; }

        /// <summary>
        /// Beviteli mező kötése
        /// </summary>
        private Expression<Func<TSource, object>> TextValue { get; set; }

        /// <summary>
        /// Láthatóság kötése
        /// </summary>
        public Expression<Func<TSource, object>> IsVisible { get; set; }

        /// <summary>
        /// Engedélyezett-e a beviteli mező kötése
        /// </summary>
        public Expression<Func<TSource, object>> IsEnabled { get; set; }

        /// <summary>
        /// A beviteli mező feletti label kötése
        /// </summary>
        public Expression<Func<TSource, object>> TitleBind { get; set; }

        #endregion

        #region private properties
        private UILabel _title;
        private LinearLayout _editFieldNativeView;
        #endregion

        public TextBoxControl(MvxFluentBindingDescriptionSet<TOwningTarget, TSource> binding, Expression<Func<TSource, object>> textValue)
        {
            Binding = binding;
            TextValue = textValue;

            KeyboardType = UIKeyboardType.Default;
            TextFieldAlign = UITextAlignment.Left;
            TextLength = 0;

            IsEnabledWithoutBind = true;
        }

        /// <summary>
        /// provide to set the Layout
        /// </summary>
        public void Init()
        {
            var layout = new LinearLayout(Orientation.Vertical)
            {
                LayoutParameters = new LayoutParameters(AutoSize.FillParent, AutoSize.WrapContent),
                SubViews = new XibFree.View[]
                {
                    new NativeView()
                    {
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)
                        {
                            Margins = new UIEdgeInsets(10,10,0,10),
                        },
                        View = _title = new UILabel()
                        {
                            TextColor = UIColor.Black,
                            //Text = TitleLabel,
                            Lines = 0,
                            LineBreakMode = UILineBreakMode.WordWrap,
                            //Font = Helpers.OpenSansCondensedLight(16)
                        }
                    }, 
                    _editFieldNativeView = new LinearLayout(Orientation.Horizontal)
                    {
                        Layer = new CALayer(){BorderColor = UIColor.Black.CGColor,BorderWidth = 1, BackgroundColor = UIColor.White.CGColor},
                        LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)
                        {
                           Margins = new UIEdgeInsets(10,10,5,10)
                        },
                        SubViews = new XibFree.View[]
                        {                      

                            new NativeView()
                            {
                                LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)
                                {
                                    Margins = new UIEdgeInsets(3,10,3,10)
                                },
                                View = EditField = new UITextField()
                                {
                                    TextColor = UIColor.Black,
                                    Font = UIFont.SystemFontOfSize(15),
                                    SecureTextEntry = IsPasswordBox,//true - in case of when password,
                                    AutocorrectionType = UITextAutocorrectionType.No,
                                    TextAlignment = TextFieldAlign 
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
                                Measurer = (native, constraint) =>
                                {
                                    var size = native.SizeThatFits(constraint);
                                    size.Height += 10;
                                    return size;
                                }
                            },                             
                        }
                    }, 
                    
                }
            };
            // bind or constans string Title
            if (TitleBind == null)
                _title.Text = TitleLabel;
            else
                Binding.Bind(_title).To(TitleBind);

            if (PlaceholderBind == null)
                EditField.Placeholder = PlaceholderText;
            else
                Binding.Bind(EditField).For(ef => ef.Placeholder).To(PlaceholderBind);

            // Keyboard type
            EditField.KeyboardType = KeyboardType;

            // visibility property
            if (IsVisible != null)
            { 
                Binding.Bind(_title).For("Gone").To(IsVisible);
                Binding.Bind(_editFieldNativeView).For(v => v.Gone).To(IsVisible).WithConversion(new NegateValueConverter(), null);
            }

            // TextValue
            Binding.Bind(EditField).To(TextValue);

            // enable property
            if (IsEnabled != null)
                Binding.Bind(EditField).For(v => v.Enabled).To(IsEnabled);

            // Never editing TextField
            if (!IsEnabledWithoutBind)
            {
                EditField.Enabled = false;
                _editFieldNativeView.Layer.BackgroundColor = UIColor.LightGray.CGColor;
            }

            // case of, when it has limit
            if (TextLength != 0)
            {
                EditField.ShouldChangeCharacters = (textField, range, replacementString) =>
                {
                    var newLength = textField.Text.Length + replacementString.Length - range.Length;
                    return newLength <= TextLength;
                };
            }

            Layout = new UILayoutHost(layout);


        }

        /// <summary>
        /// Get the whole TextBoxControlView
        /// </summary>
        /// <returns></returns>
        public UIView GetLayout()
        {
            Init();
            return Layout;
        }

        public void KeyboardScroll(UIView contentView, UIScrollView scrollView, Action pickersClose = null )
        {
            ScrollView = scrollView;
            EditField.EditingDidBegin += (sender, args) =>
            {
                if (pickersClose != null) { pickersClose.Invoke(); }
                OldScrollPos = ScrollView.ContentOffset.Y;
                if (Layout.Layer.Position.Y - ScrollView.ContentOffset.Y > contentView.Bounds.Height - 216 - 50) //64=NavigationBar 216=Keyboard  50=TopMargin
                {                
                    ScrollView.SetContentOffset(new CGPoint(0, (Layout.Layer.Position.Y) - (UIScreen.MainScreen.Bounds.Height - 216 - EditField.Bounds.Height - 10)), true);
                }
            };

            EditField.EditingDidEnd += (sender, args) => ScrollView.SetContentOffset(new CGPoint(0, OldScrollPos), true);           
        }
    }
}