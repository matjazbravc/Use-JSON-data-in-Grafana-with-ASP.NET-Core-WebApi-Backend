using GrafanaJsonWebApiDemo.Attributes;
using Newtonsoft.Json;
using System;

namespace GrafanaJsonWebApiDemo.Models
{
    /// <summary>
    /// Ordered Weather Forecast Model
    /// </summary>
    [Serializable]
    [JsonObject(IsReference = false)]
    public class WeatherForecast
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Order(1)]
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [Order(2)]
        [JsonProperty("temperatureC")]
        public int TemperatureC { get; set; }

        [Order(3)]
        [JsonProperty("temperatureF")]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [Order(4)]
        [JsonProperty("summary")]
        public string Summary { get; set; }
    }
}
