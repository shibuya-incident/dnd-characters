using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.UpdateShop
{
    public record UpdateShopRequest(
        string Name,
        string? DisplayImageUrl,
        ShopType ShopType,
        string OwnerName);
}
