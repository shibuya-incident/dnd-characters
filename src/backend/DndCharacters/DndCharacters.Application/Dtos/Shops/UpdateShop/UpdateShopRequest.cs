using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.UpdateShop
{
    public record UpdateShopRequest(
        string Name,
        string? ProfileImage,
        ShopType ShopType,
        string OwnerName);
}
