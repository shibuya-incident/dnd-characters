using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.GetShopById
{
    public record GetShopByIdResponse(
        int Id,
        string Name,
        string? DisplayImageUrl,
        ShopType ShopType,
        string OwnerName);
}

