using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GrafanaJsonWebApiDemo.Models.Query
{
	[Serializable]
	[JsonObject(IsReference = false)]
	public class QueryRequest
    {
		public QueryRequest(QueryDateTimeRange range, 
			int intervalMs, QueryTarget[] targets, OutputFormat format, int maxDataPoints)
		{
			Range = range;
			IntervalMs = intervalMs;
			Targets = targets;
			Format = format;
			MaxDataPoints = maxDataPoints;
		}

		[Required]
		[JsonProperty("format")]
		public OutputFormat Format { get; }

		[Required]
		[JsonProperty("intervalMs")]
		public int IntervalMs { get; }

		[Required]
		[JsonProperty("maxDataPoints")]
		public int MaxDataPoints { get; }

		[Required]
		[JsonProperty("range")]
		public QueryDateTimeRange Range { get; }

		[Required]
		[JsonProperty("targets")]
		public QueryTarget[] Targets { get; }
    }
}
