using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.GetShopItemById
{
    public record GetShopItemByIdResponse(
        int Id,
        int ShopId,
        int ItemId,
        string ItemName,
        ItemType ItemType,
        string ItemDescription,
        string? ShopItemDescription,
        decimal Price,
        int Stock,
        bool IsOutOfStock,
        string? ItemDisplayImageUrl);
}
