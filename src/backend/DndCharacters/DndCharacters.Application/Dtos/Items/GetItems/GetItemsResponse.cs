namespace DndCharacters.Application.Dtos.Items.GetItems
{
    public record GetItemsResponse
    {
        public IReadOnlyCollection<GetItemsListItemResponse> Items { get; set; } = [];
    }
}
