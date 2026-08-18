namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public record GetShopsItemResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ProfileImage { get; set; }
        public int ItemsCount { get; set; }
    }
}
