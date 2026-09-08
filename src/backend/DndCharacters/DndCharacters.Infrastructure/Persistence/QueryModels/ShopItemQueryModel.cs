using DndCharacters.Domain.Enum;

namespace DndCharacters.Infrastructure.Persistence.QueryModels
{
    internal sealed class ShopItemQueryModel
    {
        public int Id { get; init; }
        public int ItemId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? DisplayImageUrl { get; init; }
        public ItemType ItemType { get; init; }
        public int Stock { get; init; }
        public decimal Price { get; init; }
    }
}
