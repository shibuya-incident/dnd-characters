using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.CreateShop
{
    public record CreateShopResponse(
        int Id,
        string Name,
        string? ProfileImage,
        ShopType ShopType,
        string OwnerName);
}
