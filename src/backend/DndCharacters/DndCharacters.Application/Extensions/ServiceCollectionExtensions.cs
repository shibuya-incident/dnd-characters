using DndCharacters.Application.Interfaces;
using DndCharacters.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DndCharacters.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IShopService, ShopService>();
            return services;
        }
    }
}
