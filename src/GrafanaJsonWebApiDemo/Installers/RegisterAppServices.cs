using GrafanaJsonWebApiDemo.Contracts;
using GrafanaJsonWebApiDemo.Models;
using GrafanaJsonWebApiDemo.Services;
using GrafanaJsonWebApiDemo.Services.Converters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace GrafanaJsonWebApiDemo.Installers
{
    internal class RegisterAppServices : IServiceRegistration
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IGrafanaHelper, GrafanaHelper>();
            services.AddTransient<IWeatherForecastManager, WeatherForecastManager>();
            services.AddTransient<IConverter<IList<WeatherForecast>, GrafanaTable>, WeatherForecastToGrafanaTableConverter>();
        }
    }
}
