using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.GetShopItems
{
    public record GetShopItemsListItemResponse(
        int Id,
        string Name,
        string? DisplayImageUrl,
        ItemType ItemType,
        decimal Price);
}
