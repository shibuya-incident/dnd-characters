using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Items.UpdateItem
{
    public record UpdateItemResponse(
        int Id,
        string Name,
        string Description,
        ItemType ItemType,
        string? DisplayImageUrl);
}
