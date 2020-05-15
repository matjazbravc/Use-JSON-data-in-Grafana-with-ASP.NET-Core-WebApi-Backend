using GrafanaJsonWebApiDemo.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;

namespace GrafanaJsonWebApiDemo.Extensions
{
    /// <summary>
    /// Service extensions
    /// </summary>
    public static class ServiceExtensions
    {
        public static void AddServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var appServices = typeof(Startup).Assembly.DefinedTypes
                            .Where(x => typeof(IServiceRegistration)
                            .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                            .Select(Activator.CreateInstance)
                            .Cast<IServiceRegistration>().ToList();
            appServices.ForEach(svc => svc.Register(services, configuration));
        }
    }
}
