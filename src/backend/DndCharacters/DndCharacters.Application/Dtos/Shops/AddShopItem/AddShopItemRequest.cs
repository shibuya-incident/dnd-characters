namespace DndCharacters.Application.Dtos.Shops.AddShopItem
{
    public record AddShopItemRequest(
        string? Description,
        decimal Price,
        int Stock);
}
