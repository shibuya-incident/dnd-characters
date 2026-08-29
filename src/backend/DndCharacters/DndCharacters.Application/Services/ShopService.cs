using DndCharacters.Application.Dtos.Shops.AddShopItem;
using DndCharacters.Application.Dtos.Shops.CreateShop;
using DndCharacters.Application.Dtos.Shops.DeleteShop;
using DndCharacters.Application.Dtos.Shops.DeleteShopItem;
using DndCharacters.Application.Dtos.Shops.GetShopById;
using DndCharacters.Application.Dtos.Shops.GetShopItemById;
using DndCharacters.Application.Dtos.Shops.GetShopItems;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Dtos.Shops.UpdateShop;
using DndCharacters.Application.Dtos.Shops.UpdateShopItem;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using FluentValidation;

namespace DndCharacters.Application.Services
{
    public class ShopService(IShopRepository shopRepository, IItemRepository itemRepository) : IShopService
    {
        public async Task<AddShopItemResponse> AddShopItemAsync(int shopId, int itemId, AddShopItemRequest request)
        {
            await new AddShopItemRequestValidator().ValidateAndThrowAsync(request);

            Shop shop = await shopRepository.GetByIdAsync(shopId)
                ?? throw new KeyNotFoundException($"Shop with id {shopId} not found.");

            Item item = await itemRepository.GetByIdAsync(itemId)
                ?? throw new KeyNotFoundException($"Item with id {itemId} not found.");

            bool shopItemExist = await shopRepository.ExistAsync(shopId, itemId);

            if (shopItemExist)
            {
                throw new InvalidOperationException($"The item {itemId} already exists in shop {shopId}");
            }

            ShopItem shopItem = ShopItem.Create(
                request.Price,
                request.Stock,
                request.Description,
                shopId,
                itemId);

            shop.AddShopItem(shopItem);

            await shopRepository.UpdateAsync(shop);

            return new AddShopItemResponse(
                shopItem.Id,
                shopItem.ShopId,
                shopItem.ItemId,
                shopItem.Description,
                shopItem.Price,
                shopItem.Stock,
                shopItem.IsOutOfStock);
        }

        public async Task<CreateShopResponse> CreateAsync(CreateShopRequest request)
        {
            await new CreateShopRequestValidator().ValidateAndThrowAsync(request);

            Shop shop = Shop.Create(
                request.Name,
                request.ProfileImage,
                request.ShopType,
                request.OwnerName);

            await shopRepository.AddAsync(shop);

            return new CreateShopResponse(
                shop.Id,
                shop.Name,
                shop.DisplayImageUrl,
                shop.ShopType,
                shop.OwnerName);
        }

        public async Task DeleteAsync(DeleteShopRequest request)
        {
            Shop shop = await shopRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Shop with id {request.Id} not found.");

            await shopRepository.RemoveAsync(shop);
        }

        public async Task DeleteShopItemAsync(DeleteShopItemRequest request)
        {
            Shop shop = await shopRepository.GetByIdAsync(request.ShopId)
                ?? throw new KeyNotFoundException($"Shop with id {request.ShopId} not found.");

            Item item = await itemRepository.GetByIdAsync(request.ItemId)
                ?? throw new KeyNotFoundException($"Item with id {request.ItemId} not found.");

            ShopItem shopItem = shop.ShopItems.FirstOrDefault(x => x.ItemId == request.ItemId)
                ?? throw new InvalidOperationException($"The item {request.ItemId} doesn't exists in shop {request.ShopId}");

            await shopRepository.RemoveShopItem(shopItem);
        }

        public async Task<GetShopByIdResponse> GetByIdAsync(GetShopByIdRequest request)
        {
            Shop shop = await shopRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Shop with id {request.Id} not found.");

            return new GetShopByIdResponse(
                shop.Id,
                shop.Name,
                shop.DisplayImageUrl,
                shop.ShopType,
                shop.OwnerName);
        }

        public async Task<GetShopsResponse> GetFilteredAsync(GetShopsRequest request)
        {
            return await shopRepository.GetAsync(request);
        }

        public async Task<GetShopItemByIdResponse> GetShopItemByIdAsync(GetShopItemByIdRequest request)
        {
            GetShopItemByIdResponse shopItem = await shopRepository.GetShopItemAsync(request)
                ?? throw new KeyNotFoundException($"Shop item with id {request.ItemId} not found in shop with id {request.ShopId}.");

            return shopItem;
        }

        public async Task<GetShopItemsResponse> GetShopItemsAsync(GetShopItemsRequest request)
        {
            return await shopRepository.GetShopItemsAsync(request);
        }

        public async Task<UpdateShopResponse> UpdateAsync(int id, UpdateShopRequest request)
        {
            await new UpdateShopRequestValidator().ValidateAndThrowAsync(request);

            Shop shop = await shopRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Shop with id {id} not found.");

            shop.Name = request.Name;
            shop.DisplayImageUrl = request.ProfileImage;
            shop.ShopType = request.ShopType;
            shop.OwnerName = request.OwnerName;

            await shopRepository.UpdateAsync(shop);

            return new UpdateShopResponse(
                shop.Id,
                shop.Name,
                shop.DisplayImageUrl,
                shop.ShopType,
                shop.OwnerName);

        }

        public async Task<UpdateShopItemResponse> UpdateShopItemAsync(int shopId, int itemId, UpdateShopItemRequest request)
        {

            await new UpdateShopItemRequestValidator().ValidateAndThrowAsync(request);

            Shop shop = await shopRepository.GetByIdAsync(shopId)
                ?? throw new KeyNotFoundException($"Shop with id {shopId} not found.");

            Item item = await itemRepository.GetByIdAsync(itemId)
                ?? throw new KeyNotFoundException($"Item with id {itemId} not found.");

            ShopItem shopItem = shop.ShopItems.FirstOrDefault(x => x.ItemId == itemId)
                ?? throw new InvalidOperationException($"The item {itemId} doesn't exists in shop {shopId}");

            shopItem.Price = request.Price;
            shopItem.Stock = request.Stock;
            shopItem.Description = request.Description;

            await shopRepository.UpdateAsync(shop);

            return new UpdateShopItemResponse(
                shopItem.Id,
                shopItem.ShopId,
                shopItem.ItemId,
                shopItem.Description,
                shopItem.Price,
                shopItem.Stock,
                shopItem.IsOutOfStock);
        }
    }
}
