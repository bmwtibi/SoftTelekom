using System;
using Newtonsoft.Json;

namespace SoftTelekom.Core
{
	public class LoginModel : BaseRequestModel
	{
		[JsonProperty(PropertyName = "email")]
		public String Email { get; set; }

		[JsonProperty(PropertyName = "password")]
		public String Password { get; set; }
	}
}
