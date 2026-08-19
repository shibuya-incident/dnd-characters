using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.CreateShop
{
    public record CreateShopRequest(
        string Name,
        string? ProfileImage,
        ShopType ShopType,
        string OwnerName);
}
