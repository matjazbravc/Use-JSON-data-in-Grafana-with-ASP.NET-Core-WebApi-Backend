using Newtonsoft.Json;
using System;

namespace GrafanaJsonWebApiDemo.Models.Query
{
	[Serializable]
	[JsonObject(IsReference = false)]
	public class QueryTarget
    {
		public QueryTarget(string target)
		{
			Target = target;
		}

		[JsonProperty("target")]
		public string Target { get; }
    }
}
