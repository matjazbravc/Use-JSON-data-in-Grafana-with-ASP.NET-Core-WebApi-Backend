using GrafanaJsonWebApiDemo.Contracts;
using GrafanaJsonWebApiDemo.Models.Query;
using GrafanaJsonWebApiDemo.Models.Search;
using GrafanaJsonWebApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrafanaJsonWebApiDemo.Controllers
{
    /// <summary>
    /// Grafana WebApi Controller
    /// Read more: https://grafana.com/grafana/plugins/simpod-json-datasource
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastManager _weatherForecastManager;
        private readonly IConverter<IList<WeatherForecast>, GrafanaTable> _weatherForecastToGrafanaTableConverter;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IWeatherForecastManager weatherForecastManager,
            IConverter<IList<WeatherForecast>, GrafanaTable> weatherForecastToGrafanaTableConverter)
        {
            _logger = logger;
            _weatherForecastManager = weatherForecastManager;
            _weatherForecastToGrafanaTableConverter = weatherForecastToGrafanaTableConverter;
        }

        /// <summary>
        /// Used for "Test connection" on the datasource config page.
        /// </summary>
        /// <returns>200 OK</returns>
        [HttpGet]
        public IActionResult Get() => Ok();

        /// <summary>
        /// Return metrics based on input, in our case JSON Table
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("query")]
        public async Task<IActionResult> Query([FromBody] QueryRequest query)
        {
            var monthNumber = (int)SearchOptions.CurrentMonth;
            var target = query.Targets.FirstOrDefault();
            if (target != null)
            {
                if (int.TryParse(target.Target, out int monthNr))
                {
                    monthNumber = monthNr;
                }
            }
            var result = await _weatherForecastManager.GetAsync(monthNumber).ConfigureAwait(false);
            var grafanaTableResult = _weatherForecastToGrafanaTableConverter.Convert(result);
            var jsonResult = JsonConvert.SerializeObject(grafanaTableResult);
            return Ok($"[{jsonResult}]");
        }

        /// <summary>
        /// Return available metrics
        /// </summary>
        /// <returns>List of metrics</returns>
        [HttpPost("search")]
        public async Task<IActionResult> Search()
        {
            var result = await _weatherForecastManager.SearchOptionsAsync().ConfigureAwait(false);
            var jsonResult = JsonConvert.SerializeObject(result);
            return Ok(jsonResult);
        }
    }
}
