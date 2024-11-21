using Microsoft.EntityFrameworkCore;
using LayeredArchitecture.Infrastructure.Utils;
namespace LayeredArchitecture.API.Utils
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LayeredArchitectureContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConnection")));
            return services;
        }
    }
}
