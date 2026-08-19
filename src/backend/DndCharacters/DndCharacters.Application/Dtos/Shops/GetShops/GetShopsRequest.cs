namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public record GetShopsRequest(
        string? OrderBy,
        int Page,
        int PageSize);
}
