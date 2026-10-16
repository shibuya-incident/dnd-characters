using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.CreateShop
{
    public record CreateShopRequest(
        string Name,
        string? DisplayImageUrl,
        ShopType ShopType,
        string OwnerName);
}
