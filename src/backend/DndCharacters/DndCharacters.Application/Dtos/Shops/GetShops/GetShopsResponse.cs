namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public record GetShopsResponse
    {
        public IReadOnlyCollection<GetShopsListItemResponse> Shops { get; set; } = [];
    }
}
