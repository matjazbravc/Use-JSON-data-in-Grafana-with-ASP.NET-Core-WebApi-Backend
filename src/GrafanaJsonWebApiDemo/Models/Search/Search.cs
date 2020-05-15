using Newtonsoft.Json;
using System;

namespace GrafanaJsonWebApiDemo.Models.Search
{
    [Serializable]
    [JsonObject(IsReference = false)]
    public class Search
    {
        public Search(string text, int value)
        {
            Text = text;
            Value = value;
        }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }
}
