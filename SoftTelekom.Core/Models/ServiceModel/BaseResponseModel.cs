using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SoftTelekom.Core
{
	public class BaseResponseModel<T> where T: class, new()
	{
		[DataMember(Name = "error")]
		[JsonProperty(PropertyName = "error")]
		public bool IsError { get; set; }

		[DataMember(Name = "errorCode")]
		[JsonProperty(PropertyName = "errorCode")]
		public Int16 ErrorCode { get; set; }

		[DataMember(Name = "errorMessage")]
		[JsonProperty(PropertyName = "errorMessage")]
		public String ErrorMessage { get; set; }

		[DataMember(Name = "response")]
		[JsonProperty(PropertyName = "response")]
		public T Response { get; set; }

	}
}
