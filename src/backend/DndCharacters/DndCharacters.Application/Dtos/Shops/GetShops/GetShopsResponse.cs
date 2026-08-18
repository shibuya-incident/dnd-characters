namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public record GetShopsResponse
    {
        public IEnumerable<GetShopsItemResponse> Shops { get; set; } = [];
    }
}
