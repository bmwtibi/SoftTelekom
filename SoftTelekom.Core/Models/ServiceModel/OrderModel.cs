using System;
using Newtonsoft.Json;

namespace SoftTelekom.Core
{
	public class OrderModel : BaseRequestModel
	{

		[JsonProperty(PropertyName = "name")]
		public String Name { get; set; }

		[JsonProperty(PropertyName = "email")]
		public String Email { get; set; }

		[JsonProperty(PropertyName = "phone")]
		public String PhoneNumber { get; set; }

		[JsonProperty(PropertyName = "location")]
		public String Location { get; set; }

		[JsonProperty(PropertyName = "speed")]
		public int Speed { get; set; }
	}
}
