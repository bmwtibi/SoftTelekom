using System;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using CoreAnimation;
using CoreGraphics;
using SoftTelekom.Core.Utils;
using SoftTelekom.Core.ViewModels;
using SoftTelekom.iOS.Converters;
using SoftTelekom.iOS.Utils;
using SoftTelekom.iOS.Views.Controls;
using SoftTelekom.iOS.Views.CustomViews;
using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views
{
    public class SettingsView : MvxViewController
    {
        protected SettingsViewModel Model { get { return ViewModel as SettingsViewModel; } }
     
        public ViewGroup Layout;
      
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var langPicker = new PickerControl(true){Margin =new UIEdgeInsets(20, 10, 0, 10) };
            
            var themePicker = new PickerControl(true){Margin =new UIEdgeInsets(10, 10, 0, 10) };
            var saveButton = new ButtonControl(Helper.GetLangText("Save"), Model.SaveCommand) { LabelFontColor = UIColor.White, Margin = new UIEdgeInsets(20, 10, 0, 10) };
            var set = this.CreateBindingSet<SettingsView, SettingsViewModel>();

            Layout = new LinearLayout(Orientation.Vertical)
            {
                LayoutParameters = new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent),
                SubViews = new View[]
                {
                    new NativeView(langPicker.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(themePicker.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 
                    new NativeView(saveButton.GetView(),new LayoutParameters(AutoSize.FillParent,AutoSize.WrapContent)), 

                }
            };




            View = new UILayoutHostScrollable(Layout);
            View.BackgroundColor = UIColor.White;
            NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
            NavigationController.NavigationBar.TintColor = UIColor.White;
            set.Bind(this).For(v => v.Title).To(vm => vm.TopBarTitle);
            set.Bind(NavigationController.NavigationBar).For(t => t.BarTintColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");

            set.Bind(langPicker.ButtonControl).For(v => v.DefaultBackgroundColor).To(vm => vm.PickerColor).WithConversion("NativeColor");
            set.Bind(langPicker.ButtonControl.Label).To(vm => vm.SelectedLanguage).WithConversion(new EnumToStringValueConverter());
            set.Bind(langPicker.EnumModel).For(v => v.ItemsSource).To(vm => vm.LanguagesList);
            set.Bind(langPicker.EnumModel).For(v => v.SelectedItem).To(vm => vm.SelectedLanguage);
            set.Bind(langPicker.EnumModel).For(v => v.SelectedChangedCommand).To(vm => vm.SelectedLanguageCommand);

            set.Bind(themePicker.ButtonControl).For(v => v.DefaultBackgroundColor).To(vm => vm.PickerColor).WithConversion("NativeColor");
            set.Bind(themePicker.ButtonControl.Label).To(vm => vm.SelectedTheme).WithConversion(new EnumToStringValueConverter());
            set.Bind(themePicker.EnumModel).For(v => v.ItemsSource).To(vm => vm.ThemeList);
            set.Bind(themePicker.EnumModel).For(v => v.SelectedItem).To(vm => vm.SelectedTheme);
            set.Bind(themePicker.EnumModel).For(v => v.SelectedChangedCommand).To(vm => vm.SelectedThemeCommand);

            set.Bind(saveButton).For(v => v.DefaultBackgroundColor).To(vm => vm.TopBarColor).WithConversion("NativeColor");

            set.Apply();
        }
    }
}