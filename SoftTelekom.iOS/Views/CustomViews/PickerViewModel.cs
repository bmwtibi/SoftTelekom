using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.CrossCore.WeakSubscription;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using SoftTelekom.Core.Models;
using UIKit;

namespace SoftTelekom.iOS.Views.CustomViews
{
    public class PickerViewModel : UIPickerViewModel
    {
        private readonly UIPickerView _pickerView;
        private ObservableCollection<CityItem> _itemsSourceCollectionCity;
        private ObservableCollection<SpeedItem> _itemsSourceCollectionSpeed;
        private IDisposable _subscription;
        private object _selectedItem;

        public ObservableCollection<CityItem> ItemsSourceCollectionCity
        {
            get
            {
                return this._itemsSourceCollectionCity;
            }
            set
            {
                this._itemsSourceCollectionCity = value;
                INotifyCollectionChanged collectionChanged = this._itemsSourceCollectionCity as INotifyCollectionChanged;
                if (collectionChanged != null)
                    this._subscription = (IDisposable)MvxWeakSubscriptionExtensionMethods.WeakSubscribe(collectionChanged,
                                new EventHandler<NotifyCollectionChangedEventArgs>(this.CollectionChangedOnCollectionChanged));
                this.Reload();
                this.ShowSelectedItem();
            }
        }

        public ObservableCollection<SpeedItem> ItemsSourceCollectionSpeed
        {
            get
            {
                return this._itemsSourceCollectionSpeed;
            }
            set
            {
                this._itemsSourceCollectionSpeed = value;
                INotifyCollectionChanged collectionChanged = this._itemsSourceCollectionSpeed as INotifyCollectionChanged;
                if (collectionChanged != null)
                    this._subscription = (IDisposable)MvxWeakSubscriptionExtensionMethods.WeakSubscribe(collectionChanged,
                                new EventHandler<NotifyCollectionChangedEventArgs>(this.CollectionChangedOnCollectionChanged));
                this.Reload();
                this.ShowSelectedItem();
            }
        }

        public List<string> PropertyName;

        public object SelectedItem
        {
            get
            {
                //if (_selectedItem == null)
                //{
                //    if (_itemsSourceCollectionScheme != null)
                //    {
                //        _selectedItem = _itemsSourceCollectionScheme[0];
                //    }
                //}
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

        public PickerViewModel(UIPickerView pickerView)
        {
            this._pickerView = pickerView;
            _pickerView.Model = this;

            PropertyName = new List<string>();
        }

        public PickerViewModel()
        {

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
            return this._itemsSourceCollectionCity == null ? MvxEnumerableExtensions.Count(this._itemsSourceCollectionSpeed) : MvxEnumerableExtensions.Count(this._itemsSourceCollectionCity);
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {

            if (this._itemsSourceCollectionCity != null)
            {
                this._selectedItem = MvxEnumerableExtensions.ElementAt(this._itemsSourceCollectionCity, (int)row);
            }
            else
            {
                this._selectedItem = MvxEnumerableExtensions.ElementAt(this._itemsSourceCollectionSpeed, (int)row);
            }

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
            if (this._itemsSourceCollectionCity == null)
                return;

            int position;

            if (this._itemsSourceCollectionCity != null)
            {
                position = MvxEnumerableExtensions.GetPosition(this._itemsSourceCollectionCity, this._selectedItem);
            }
            else
            {
                position = MvxEnumerableExtensions.GetPosition(this._itemsSourceCollectionSpeed, this._selectedItem);
            }

            if (position < 0)
                return;
            bool animated = !this._pickerView.Hidden;
            // ISSUE: reference to a compiler-generated method
            this._pickerView.Select(position, 0, animated);
        }


        private readonly IDictionary<string, Func<String>> _cache = new Dictionary<string, Func<string>>();

        public override UIView GetView(UIPickerView picker, nint row, nint component, UIView view)
        {
            UIView _view = new UIView();

            UILabel label = new UILabel();

            if (ItemsSourceCollectionCity != null)
            {
                label.Text = ItemsSourceCollectionCity[(int)row].Name; //PropertyName[row];
            }
            // EntryScheme case
            else
            {
                label.Text = ItemsSourceCollectionSpeed[(int)row].Name;
            }

            label.TextColor = UIColor.Black;
            label.LineBreakMode = UILineBreakMode.WordWrap;
            label.TextAlignment = UITextAlignment.Center;
            label.Lines = 0;

            _view.Bounds = new RectangleF(0, 0, 280, 50);
            label.Frame = new RectangleF(0, 0, 280, 50);
            label.Font = UIFont.FromName("HelveticaNeue", 19f);

            _view.AddSubview(label);
            return _view;
        }

        public override nfloat GetRowHeight(UIPickerView picker, nint component)
        {
            return 50F;
        }

    }
}