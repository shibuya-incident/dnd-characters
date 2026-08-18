using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Services
{
    public class ShopService(IShopRepository shopRepository) : IShopService
    {
        public GetShopsResponse GetShops(GetShopsRequest request)
        {
            IEnumerable<Shop> shops = shopRepository.Get(request);

            //Map the shops (Domain model) to the response DTO
            return new GetShopsResponse
            {
                Shops = [.. shops.Select(shop => new GetShopsItemResponse
                {
                    Id = shop.Id,
                    Name = shop.Name,
                    ProfileImage = shop.ProfileImage,
                    ItemsCount = shop.Items.Count
                })]
            };
        }
    }
}
