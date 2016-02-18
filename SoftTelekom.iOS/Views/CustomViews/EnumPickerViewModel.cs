using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.CrossCore.WeakSubscription;
using Cirrious.MvvmCross.Binding.Attributes;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Cirrious.MvvmCross.Localization;
using CoreGraphics;
using SoftTelekom.Core.Utils;
using UIKit;

namespace SoftTelekom.iOS.Views.CustomViews
{
    public class EnumPickerViewModel : UIPickerViewModel
    {
        private readonly UIPickerView _pickerView;
        private IEnumerable _itemsSource;
        private IDisposable _subscription;
        private object _selectedItem;
        private readonly MvxLanguageBinder _textSource;

        [MvxSetToNullAfterBinding]
        public virtual IEnumerable ItemsSource
        {
            get
            {
                return this._itemsSource;
            }
            set
            {
                if (this._itemsSource == value)
                    return;
                if (this._subscription != null)
                {
                    this._subscription.Dispose();
                    this._subscription = (IDisposable)null;
                }
                this._itemsSource = value;
                INotifyCollectionChanged collectionChanged = this._itemsSource as INotifyCollectionChanged;
                if (collectionChanged != null)
                    this._subscription =
                        (IDisposable)
                            MvxWeakSubscriptionExtensionMethods.WeakSubscribe(collectionChanged,
                                new EventHandler<NotifyCollectionChangedEventArgs>(
                                    this.CollectionChangedOnCollectionChanged));
                this.Reload();
                this.ShowSelectedItem();
            }
        }

        public UIImage imagePicker { get; set; }

        public object SelectedItem
        {
            get
            {
                return this._selectedItem;
            }
            set
            {
                this._selectedItem = value;
                this.ShowSelectedItem();
            }
        }

        public ICommand SelectedChangedCommand { get; set; }

        public event EventHandler SelectedItemChanged;

        public EnumPickerViewModel(UIPickerView pickerView)
        {
            this._pickerView = pickerView;
            _pickerView.Model = this;
            _textSource = new MvxLanguageBinder(Constants.GeneralNamespace, Constants.Shared);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this._subscription != null)
            {
                this._subscription.Dispose();
                this._subscription = (IDisposable)null;
            }
            base.Dispose(disposing);
        }

        protected virtual void CollectionChangedOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Mvx.Trace(
                "CollectionChanged called inside MvxPickerViewModel - beware that this isn't fully tested - picker might not fully support changes while the picker is visible",
                new object[0]);
            this.Reload();
        }

        protected virtual void Reload()
        {
            // ISSUE: reference to a compiler-generated method
            this._pickerView.ReloadComponent(0);
        }

        public override nint GetComponentCount(UIPickerView picker)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView picker, nint component)
        {
            return this._itemsSource == null ? 0 : MvxEnumerableExtensions.Count(this._itemsSource);
        }

        protected virtual string RowTitle(int row, object item)
        {
            return _textSource.GetText(item.ToString());
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {
            this._selectedItem = MvxEnumerableExtensions.ElementAt(this._itemsSource, (int)row);
            EventHandler eventHandler = this.SelectedItemChanged;
            if (eventHandler != null)
                eventHandler((object)this, EventArgs.Empty);
            ICommand selectedChangedCommand = this.SelectedChangedCommand;
            if (selectedChangedCommand == null || !selectedChangedCommand.CanExecute(this._selectedItem))
                return;
            selectedChangedCommand.Execute(this._selectedItem);
        }

        protected virtual void ShowSelectedItem()
        {
            if (this._itemsSource == null)
                return;
            int position = MvxEnumerableExtensions.GetPosition(this._itemsSource, this._selectedItem);
            if (position < 0)
                return;
            bool animated = !this._pickerView.Hidden;
            // ISSUE: reference to a compiler-generated method
            this._pickerView.Select(position, 0, animated);
        }

        public override UIView GetView(UIPickerView picker, nint row, nint component, UIView view)
        {
            UIView _view = new UIView();

            UILabel label = new UILabel();

            label.Text = this.RowTitle((int)row, MvxEnumerableExtensions.ElementAt(this._itemsSource, (int)row));

            label.TextColor = UIColor.Black;
            label.LineBreakMode = UILineBreakMode.WordWrap;
            label.TextAlignment = UITextAlignment.Center;
            label.Lines = 0;
            

            _view.Bounds = new CGRect(0, 0, 280, 30);
            label.Frame = new CGRect(0, 0, 280, 30);
            label.Font = UIFont.SystemFontOfSize(15);

            _view.AddSubview(label);
            return _view;
        }

        public override nfloat GetRowHeight(UIPickerView picker, nint component)
        {
            return 30f;
        }
    }
}