using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace GrafanaJsonWebApiDemo.Models
{
    /// <summary>
    /// Grafana Table Model
    /// </summary>
    [Serializable]
    [JsonObject(IsReference = false)]
    public class GrafanaTable
    {
        public GrafanaTable()
        {
            Columns = new List<Column>();
            Rows = new List<IList<object>>();
        }

        [JsonProperty("type")]
        public string Type { get; set; } = "table";

        [JsonProperty("columns")]
        public IList<Column> Columns { get; set; }

        [JsonProperty("rows")]
        public IList<IList<object>> Rows { get; set; }
    }
}
