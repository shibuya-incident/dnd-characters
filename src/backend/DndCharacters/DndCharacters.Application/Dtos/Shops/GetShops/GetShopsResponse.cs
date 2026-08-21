namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public record GetShopsResponse
    {
        public IEnumerable<GetShopsListItemResponse> Shops { get; set; } = [];
    }
}
