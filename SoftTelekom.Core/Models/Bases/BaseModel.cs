using Cirrious.CrossCore.UI;
using Cirrious.MvvmCross.Localization;
using Cirrious.MvvmCross.ViewModels;
using Newtonsoft.Json;

namespace SoftTelekom.Core.Models.Bases
{
    public class BaseModel : MvxNotifyPropertyChanged
    {
        [JsonIgnore]
        private bool _isSelected;
        [JsonIgnore]
        public bool IsSelected { get { return _isSelected; } set { _isSelected = value; RaisePropertyChanged(() => IsSelected); } }
        [JsonIgnore]
        public string GridImage { get; set; }
        [JsonIgnore]
        public MvxColor ListBackgroundColor { get; set; }
        public MvxColor ListTextColor { get; set; }
    }
}