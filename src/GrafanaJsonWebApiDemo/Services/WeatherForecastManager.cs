using GrafanaJsonWebApiDemo.Contracts;
using GrafanaJsonWebApiDemo.Models;
using GrafanaJsonWebApiDemo.Models.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrafanaJsonWebApiDemo.Services
{
    /// <summary>
    /// Weather Forecast Manager for generating random forecasts
    /// </summary>
    public class WeatherForecastManager : IWeatherForecastManager
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<IList<WeatherForecast>> GetAsync(int monthNumber = 1)
        {
            // Current Year
            var year = DateTime.Now.Year;

            monthNumber = DateTime.Now.AddMonths(monthNumber-1).Month;

            // Get first Day in a Month
            var firstDay = new DateTime(year, monthNumber, 1);
            var daysInMonth = DateTime.DaysInMonth(year, monthNumber);

            var rng = new Random();
            var result = Enumerable.Range(0, daysInMonth).Select(day => new WeatherForecast
            {
                Id = day,
                Date = firstDay.AddDays(day),
                TemperatureC = rng.Next(-15, 35),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).OrderBy(wf => wf.Date).ToList();

            return await Task.FromResult(result);
        }

        public async Task<List<Search>> SearchOptionsAsync()
        {
            var result = new List<Search>();
            result.AddRange(new List<Search>
            {
                new Search("Current Month", (int)SearchOptions.CurrentMonth),
                new Search("Next Month", (int)SearchOptions.NextMonth)
            });
            return await Task.FromResult(result);
        }
    }
}
