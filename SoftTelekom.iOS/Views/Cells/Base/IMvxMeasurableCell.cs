using UIKit;
using XibFree;

namespace SoftTelekom.iOS.Views.Cells.Base
{
    interface IMvxMeasurableCell<in T>
    {


        ViewGroup Layout
        {
            get;
            set;
        }

        void Initialize();

        void Init(T item);

        float MeasureHeight(UITableView tableView, T item);
    }
}