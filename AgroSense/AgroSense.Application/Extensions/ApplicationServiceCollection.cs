using AgroSense.Application.Interfaces.Services;
using AgroSense.Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace AgroSense.Application.Extensions
{
    public static class ApplicationServiceCollection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IAreaService, AreaService>();

            return services;
        }
    }
}
