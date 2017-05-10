using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SoftTelekom.Core
{
	public class NewsModel
	{
		[DataMember(Name = "idnews")]
		[JsonProperty(PropertyName = "idnews")]
		public Int16 Id { get; set; }

		[DataMember(Name = "newstitle")]
		[JsonProperty(PropertyName = "newstitle")]
		public String Title { get; set; }

		[DataMember(Name = "newsmessage")]
		[JsonProperty(PropertyName = "newsmessage")]
		public String Message { get; set; }

		[DataMember(Name = "newsdate")]
		[JsonProperty(PropertyName = "newsdate")]
		public DateTime DateTime { get; set; }
	}
}
