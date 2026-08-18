namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public record GetShopsRequest
    {
        public string? OrderBy { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
