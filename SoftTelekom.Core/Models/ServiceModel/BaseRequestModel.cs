using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SoftTelekom.Core
{
	public class BaseRequestModel
	{
		//[DataMember(Name = "clientId")]
		[JsonProperty(PropertyName = "clientId")]
		public String ClientId = "aU9TLWNsb3VkaW5nLXNlcnZpY2U=";
	}
}
