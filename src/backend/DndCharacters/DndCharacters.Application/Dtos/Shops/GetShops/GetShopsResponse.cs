namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public record GetShopsResponse
    {
        public IReadOnlyCollection<GetShopsListItemResponse> Shops { get; set; } = [];
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
