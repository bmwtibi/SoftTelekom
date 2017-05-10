using System;
using Newtonsoft.Json;

namespace SoftTelekom.Core
{
	public class OrderResponseModel
	{
		
		[JsonProperty(PropertyName = "issucces")]
		public bool IsSucces { get; set; }
	}
}
