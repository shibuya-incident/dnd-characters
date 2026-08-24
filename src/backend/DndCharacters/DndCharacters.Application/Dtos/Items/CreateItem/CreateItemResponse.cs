using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Items.CreateItem
{
    public record CreateItemResponse(
        int Id,
        string Name,
        string Description,
        ItemType ItemType,
        string? DisplayImageUrl);
}
