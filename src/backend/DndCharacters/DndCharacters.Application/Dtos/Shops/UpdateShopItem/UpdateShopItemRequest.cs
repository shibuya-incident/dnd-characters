namespace DndCharacters.Application.Dtos.Shops.UpdateShopItem
{
    public record UpdateShopItemRequest(
        string? Description,
        decimal Price,
        int Stock);
}
