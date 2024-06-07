using InnovaWideTest.Application.Services.AuthServices;
using InnovaWideTest.Application.Services.TenantServices;
using Microsoft.Extensions.DependencyInjection;

namespace InnovaWideTest.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
