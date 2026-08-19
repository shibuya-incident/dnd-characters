using DndCharacters.Application.Interfaces;
using DndCharacters.Infrastructure.Constants;
using DndCharacters.Infrastructure.Persistence;
using DndCharacters.Infrastructure.Persistence.Shops;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DndCharacters.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IShopRepository, ShopMockRepository>();

            services.AddPersistence(configuration);
            return services;
        }

        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(InfrastructureConfiguration.DefaultConnectionStringName));
            });
        }

    }
}
