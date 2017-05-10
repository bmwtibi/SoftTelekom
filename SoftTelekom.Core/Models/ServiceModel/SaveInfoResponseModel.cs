using System;
using Newtonsoft.Json;

namespace SoftTelekom.Core
{
	public class SaveInfoResponseModel
	{
		[JsonProperty(PropertyName = "issucces")]
		public bool IsSucces { get; set; }
	}
}
