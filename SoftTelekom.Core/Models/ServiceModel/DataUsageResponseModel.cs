using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SoftTelekom.Core.Models;

namespace SoftTelekom.Core
{
	public class DataUsageResponseModel
	{
		[JsonProperty(PropertyName = "todayData")]
		public int TodayData { get; set; }

		[JsonProperty(PropertyName = "currentMonthData")]
		public int CurrentMonthData { get; set; }

		[JsonProperty(PropertyName = "oldData")]
		public List<InternetUsageItem> OldData { get; set; }
	}
}
