namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public record GetShopsItemResponse(
        int Id,
        string Name,
        string? ProfileImage,
        int ItemsCount);
}
