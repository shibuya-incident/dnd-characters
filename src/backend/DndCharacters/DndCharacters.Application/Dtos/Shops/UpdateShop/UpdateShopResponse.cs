using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.UpdateShop
{
    public record UpdateShopResponse(
        int Id,
        string Name,
        string? DisplayImageUrl,
        ShopType ShopType,
        string OwnerName);
}
