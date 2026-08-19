using DndCharacters.Application.Dtos.Shops.CreateShop;
using DndCharacters.Application.Dtos.Shops.GetShopById;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;

namespace DndCharacters.Application.Services
{
    public class ShopService(IShopRepository shopRepository) : IShopService
    {
        public CreateShopResponse Create(CreateShopRequest request)
        {
            Shop shop = Shop.Create(
                request.Name,
                request.ProfileImage,
                request.ShopType,
                request.OwnerName);

            Shop newShop = shopRepository.Add(shop);

            return new CreateShopResponse(newShop.Id, newShop.Name, newShop.ProfileImage, newShop.ShopType, newShop.OwnerName);
        }

        public GetShopByIdResponse GetById(GetShopByIdRequest request)
        {
            Shop? shop = shopRepository.GetById(request.Id)
                ?? throw new Exception($"Shop with id {request.Id} not found.");

            return new GetShopByIdResponse(
                shop.Id,
                shop.Name,
                shop.ProfileImage,
                shop.ShopType,
                shop.OwnerName);
        }

        public GetShopsResponse GetFiltered(GetShopsRequest request)
        {
            IEnumerable<Shop> shops = shopRepository.Get(request);

            //Map the shops (Domain model) to the response DTO
            return new GetShopsResponse
            {
                Shops = [.. shops.Select(shop => new GetShopsItemResponse(shop.Id, shop.Name, shop.ProfileImage, shop.Items.Count))]
            };
        }
    }
}
