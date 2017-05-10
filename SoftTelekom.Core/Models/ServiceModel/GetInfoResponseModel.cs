using System;
using Newtonsoft.Json;

namespace SoftTelekom.Core
{
	public class GetInfoResponseModel
	{
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
