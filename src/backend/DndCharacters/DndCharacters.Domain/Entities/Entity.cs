namespace DndCharacters.Domain.Entities
{
    public class Entity
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; private set; }

        protected void MarkAsUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
