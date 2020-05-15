using Newtonsoft.Json;
using System;

namespace GrafanaJsonWebApiDemo.Models
{
    /// <summary>
    /// Grafana Column Model
    /// </summary>
    [Serializable]
    [JsonObject(IsReference = false)]
    public class Column
    {
        public Column(string text, string type = "string")
        {
            Text = text;
            Type = type;
        }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } = "string";
    }
}
