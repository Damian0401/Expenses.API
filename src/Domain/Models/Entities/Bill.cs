namespace Domain.Models.Entities
{
    public class Bill
    {
        public Guid Id { get; set; }
        public double Value { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public Status Status { get; set; }
        public string OwnerId { get; set; } = null!;

        public virtual ApplicationUser Owner { get; set; } = null!;
    }
}