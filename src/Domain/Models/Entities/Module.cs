namespace Domain.Models.Entities
{
    public class Module
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Room> Rooms { get; set; } = null!;
    }
}