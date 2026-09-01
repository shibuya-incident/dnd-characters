namespace DndCharacters.Domain.Entities
{
    public class Entity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public uint RowVersion { get; private set; }

        public Entity()
        {
            var utcNow = DateTime.UtcNow;
            CreatedAt = utcNow;
            UpdatedAt = utcNow;
        }
    }
}
