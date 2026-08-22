using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public record GetShopsListItemResponse(
        int Id,
        string Name,
        ShopType ShopType,
        string? DisplayImageUrl,
        int ItemsCount);
}
