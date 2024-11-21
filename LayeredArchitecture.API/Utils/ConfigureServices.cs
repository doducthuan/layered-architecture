using Microsoft.EntityFrameworkCore;
using LayeredArchitecture.Infrastructure.Utils;
using LayeredArchitecture.Application.Services.IServices;
using LayeredArchitecture.Application.Services;
using LayeredArchitecture.Domain.IReponsitories;
using LayeredArchitecture.Infrastructure.Reponsitories;
namespace LayeredArchitecture.API.Utils
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LayeredArchitectureContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConnection"),
                builder => builder.MigrationsAssembly("LayeredArchitecture.Infrastructure")));

            #region scope service
            services.AddScoped<IAccountService, AccountService>();
            #endregion

            #region scope repository
            services.AddScoped<IAccountRepository, AccountRepository>();
            #endregion
            return services;
        }
    }
}
