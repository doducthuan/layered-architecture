using Microsoft.EntityFrameworkCore;
using LayeredArchitecture.Infrastructure.Utils;
using LayeredArchitecture.Application.Services.IServices;
using LayeredArchitecture.Application.Services;
using LayeredArchitecture.Domain.IReponsitories;
using LayeredArchitecture.Infrastructure.Reponsitories;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace LayeredArchitecture.API.Utils
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LayeredArchitectureContext>(options => options
            .UseSqlServer(configuration.GetConnectionString("DBConnection"),builder => builder.MigrationsAssembly("LayeredArchitecture.Infrastructure"))
            .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

            services.AddMemoryCache();
            #region scope service
            services.AddScoped<IStudentService, StudentService>();
            #endregion

            #region scope repository
            services.AddScoped<IStudentRepository, StudentRepository>();
            #endregion
            return services;
        }
    }
}
