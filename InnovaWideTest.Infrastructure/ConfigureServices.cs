
using InnovaWideTest.Application.Common.Interfaces.Repositories;
using InnovaWideTest.Infrastructure.Persistence;
using InnovaWideTest.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InnovaWideTest.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped<ApplicationDbContext>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICaseRepository, CaseRepository>();
            services.AddScoped<IHearingRepository, HearingRepository>();
            services.AddScoped<ILawyerRepository, LawyerRepository>();

            return services;
        }
    }
}
