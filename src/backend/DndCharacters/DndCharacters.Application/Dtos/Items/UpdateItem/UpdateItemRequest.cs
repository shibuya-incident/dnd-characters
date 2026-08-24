using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Items.UpdateItem
{
    public record UpdateItemRequest(
        string Name,
        string Description,
        ItemType ItemType,
        string? DisplayImageUrl);
}
