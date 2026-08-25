using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.GetShopItems
{
    public record GetShopItemsListItemResponse(
        int Id,
        int ItemId,
        string Name,
        string? DisplayImageUrl,
        ItemType ItemType,
        int Stock,
        decimal Price);
}
