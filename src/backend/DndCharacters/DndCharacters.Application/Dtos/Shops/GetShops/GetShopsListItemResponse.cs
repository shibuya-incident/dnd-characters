namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public record GetShopsListItemResponse(
        int Id,
        string Name,
        string? ProfileImage,
        int ItemsCount);
}
