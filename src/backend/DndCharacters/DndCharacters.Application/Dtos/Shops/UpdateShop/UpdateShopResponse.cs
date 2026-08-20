using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.UpdateShop
{
    public record UpdateShopResponse(
        int Id,
        string Name,
        string? ProfileImage,
        ShopType ShopType,
        string OwnerName);
}
