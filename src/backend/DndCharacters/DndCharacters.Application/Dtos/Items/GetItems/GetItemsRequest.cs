namespace DndCharacters.Application.Dtos.Items.GetItems
{
    public record GetItemsRequest(
        string? OrderBy,
        int Page,
        int PageSize,
        int? ItemCount);
}
