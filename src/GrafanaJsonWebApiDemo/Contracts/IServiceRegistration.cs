using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GrafanaJsonWebApiDemo.Contracts
{
    public interface IServiceRegistration
    {
        void Register(IServiceCollection services, IConfiguration configuration);
    }
}
