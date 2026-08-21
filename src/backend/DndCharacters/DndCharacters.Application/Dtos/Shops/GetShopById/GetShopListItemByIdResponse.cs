using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.GetShopById
{
    public record GetShopListItemByIdResponse(
        int Id,
        string Name,
        string? DisplayImageUrl,
        ItemType ItemType,
        string? Description,
        decimal Price,
        int Stock,
        bool IsOutOfStock);


}

