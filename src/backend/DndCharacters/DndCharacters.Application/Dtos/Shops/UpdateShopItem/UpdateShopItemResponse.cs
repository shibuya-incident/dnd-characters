namespace DndCharacters.Application.Dtos.Shops.UpdateShopItem
{
    public record UpdateShopItemResponse(
        int Id,
        int ShopId,
        int ItemId,
        string? Description,
        decimal Price,
        int Stock,
        bool IsOutOfStock);
}
