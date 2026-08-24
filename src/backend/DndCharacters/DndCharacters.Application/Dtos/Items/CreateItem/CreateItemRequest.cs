using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Items.CreateItem
{
    public record CreateItemRequest(
        string Name,
        string Description,
        ItemType ItemType,
        string? DisplayImageUrl);
}
