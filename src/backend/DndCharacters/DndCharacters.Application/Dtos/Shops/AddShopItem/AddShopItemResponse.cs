namespace DndCharacters.Application.Dtos.Shops.AddShopItem
{
    public record AddShopItemResponse(
        int Id,
        int ShopId,
        int ItemId,
        string? Description,
        decimal Price,
        int Stock,
        bool IsOutOfStock);
}
