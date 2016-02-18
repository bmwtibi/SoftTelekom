using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace SoftTelekom.Core.Utils
{
    public class TrulyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        public TrulyObservableCollection()
        {
            CollectionChanged += TrulyObservableCollection_Changed;
        }

        public TrulyObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            foreach (T item in collection)
                item.PropertyChanged += ItemPropertyChanged;

            CollectionChanged += TrulyObservableCollection_Changed;
        }

        public virtual void TrulyObservableCollection_Changed(object sender, NotifyCollectionChangedEventArgs arg)
        {
            if (arg.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (T item in Items)
                {
                    item.PropertyChanged -= ItemPropertyChanged;
                }
            }
            else if (arg.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (T item in arg.NewItems)
                {
                    item.PropertyChanged += ItemPropertyChanged;
                }
            }
        }

        public virtual void ItemPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var eventArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(eventArgs);
        }
    }
}