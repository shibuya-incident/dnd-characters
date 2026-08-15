namespace DndCharacters.Domain.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

    }
}
