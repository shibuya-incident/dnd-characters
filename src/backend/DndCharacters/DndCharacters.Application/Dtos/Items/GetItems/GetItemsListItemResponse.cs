using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Items.GetItems
{
    public record GetItemsListItemResponse(
        int Id,
        string Name,
        ItemType ItemType,
        string? DisplayImageUrl);
}
