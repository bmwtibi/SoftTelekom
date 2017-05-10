using System;
using Newtonsoft.Json;
using SoftTelekom.Core.Models.Bases;

namespace SoftTelekom.Core.Models
{
    public class InternetUsageItem : BaseModel
    {
        public DateTime Date { get; set; }

		[JsonProperty(PropertyName = "DataUsage")]
        public int DataUsage { get; set; }

		private int _year = 0;

		[JsonProperty(PropertyName = "Year")]
		public int Year
		{
			get { return _year; }
			set { 
				_year = value;
				if (_month != 0) Date = new DateTime(_year, _month, 1);
				}
		}

		private int _month = 0;
			
		[JsonProperty(PropertyName = "Month")]
		public int Month
		{
			get { return _month; }
			set
			{
				_month = value;
				if (_year != 0) Date = new DateTime(_year, _month, 1);
			}
		}
    }
}