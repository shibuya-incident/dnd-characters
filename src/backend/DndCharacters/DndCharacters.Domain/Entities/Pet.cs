namespace DndCharacters.Domain.Entities
{
    public class Pet
    {
        public required string Name { get; set; }
        public required string Species { get; set; }
        public required string Lore { get; set; }
        public string? DisplayImageUrl { get; set; }

    }
}
