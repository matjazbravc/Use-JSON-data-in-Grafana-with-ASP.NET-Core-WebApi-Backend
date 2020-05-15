using GrafanaJsonWebApiDemo.Models;
using GrafanaJsonWebApiDemo.Models.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrafanaJsonWebApiDemo.Contracts
{
    public interface IWeatherForecastManager
    {
        Task<IList<WeatherForecast>> GetAsync(int monthNumber = 0);

        Task<List<Search>> SearchOptionsAsync();
    }
}
