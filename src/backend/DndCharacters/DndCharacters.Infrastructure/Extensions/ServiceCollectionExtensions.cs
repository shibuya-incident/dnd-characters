using DndCharacters.Application.Interfaces;
using DndCharacters.Infrastructure.Persistence.Shops;
using Microsoft.Extensions.DependencyInjection;

namespace DndCharacters.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IShopRepository, ShopMockRepository>();
            return services;
        }

    }
}
