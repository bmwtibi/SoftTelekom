using System;
using Newtonsoft.Json;

namespace SoftTelekom.Core
{
	public class SaveInfoModel : BaseRequestModel
	{
		[JsonProperty(PropertyName = "token")]
		public String Token { get; set; }

		[JsonProperty(PropertyName = "name")]
		public String Name { get; set; }

		[JsonProperty(PropertyName = "email")]
		public String Email { get; set; }

		[JsonProperty(PropertyName = "address")]
		public String Address { get; set; }

		[JsonProperty(PropertyName = "phone")]
		public String Phone { get; set; }
	}
}
