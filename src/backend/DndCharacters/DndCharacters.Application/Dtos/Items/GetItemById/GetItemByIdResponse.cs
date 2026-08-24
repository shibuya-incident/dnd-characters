using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Items.GetItemById
{
    public record GetItemByIdResponse(
        int Id,
        string Name,
        string Description,
        ItemType ItemType,
        string? DisplayImageUrl);
}
