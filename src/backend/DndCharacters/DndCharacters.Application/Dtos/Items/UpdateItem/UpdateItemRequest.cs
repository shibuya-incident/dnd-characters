namespace DndCharacters.Application.Dtos.Items.UpdateItem
{
    public record UpdateItemRequest(
        string Name,
        string Description,
        string? DisplayImageUrl);
}
