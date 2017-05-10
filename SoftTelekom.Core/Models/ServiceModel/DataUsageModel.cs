using System;
using Newtonsoft.Json;

namespace SoftTelekom.Core
{
	public class DataUsageModel : BaseRequestModel
	{
		[JsonProperty(PropertyName = "token")]
		public String Token { get; set; }

	}
}
