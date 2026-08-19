using DndCharacters.Application.Dtos.Shops.CreateShop;
using DndCharacters.Application.Dtos.Shops.GetShopById;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Services
{
    public class ShopService(IShopRepository shopRepository) : IShopService
    {
        public async Task<CreateShopResponse> CreateAsync(CreateShopRequest request)
        {
            Shop shop = Shop.Create(
                request.Name,
                request.ProfileImage,
                request.ShopType,
                request.OwnerName);

            await shopRepository.AddAsync(shop);

            return new CreateShopResponse(shop.Id, shop.Name, shop.ProfileImage, shop.ShopType, shop.OwnerName);
        }

        public async Task<GetShopByIdResponse> GetByIdAsync(GetShopByIdRequest request)
        {
            Shop? shop = await shopRepository.GetByIdAsync(request.Id)
                ?? throw new Exception($"Shop with id {request.Id} not found.");

            return new GetShopByIdResponse(
                shop.Id,
                shop.Name,
                shop.ProfileImage,
                shop.ShopType,
                shop.OwnerName);
        }

        public async Task<GetShopsResponse> GetFilteredAsync(GetShopsRequest request)
        {
            IEnumerable<Shop> shops = await shopRepository.GetAsync(request);

            return new GetShopsResponse
            {
                Shops = [.. shops.Select(shop => new GetShopsItemResponse(shop.Id, shop.Name, shop.ProfileImage, shop.Items.Count))]
            };
        }
    }
}
