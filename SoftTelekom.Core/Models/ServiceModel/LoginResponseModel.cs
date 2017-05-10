using System;
using Newtonsoft.Json;

namespace SoftTelekom.Core
{
	public class LoginResponseModel
	{	
		[JsonProperty(PropertyName = "token")]
		public string Token { get; set; }
	}
}
