using System;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using SoftTelekom.iOS.Views.Cells.Base;
using UIKit;

namespace SoftTelekom.iOS.DataSources
{
    internal class GenericDataSource<TItem, TCell> : MvxTableViewSource
        where TCell : MvxTableViewCell, IMvxMeasurableCell<TItem>, new()
        where TItem : class, new()
    {
        private readonly TCell _prototype;
        public static readonly NSString Key = new NSString(typeof(TCell).ToString());
        public GenericDataSource(UITableView tableView)
            : base(tableView)
        {
            _prototype = new TCell();
            _prototype.Initialize();
            tableView.RegisterClassForCellReuse(typeof(TCell), Key);
        }

        public GenericDataSource(IntPtr handle)
            : base(handle)
        {
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return _prototype.MeasureHeight(tableView, ItemsSource.ElementAt(indexPath.Row) as TItem);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = TableView.DequeueReusableCell(Key, indexPath);
            ((TCell)cell).Initialize();
            return cell;
        }
    }
}