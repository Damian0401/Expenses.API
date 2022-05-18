namespace Domain.Models.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid ModuleId { get; set; }
        public int MaxResidentNumber { get; set; }

        public virtual Module? Module { get; set; } = null!;
        public virtual ICollection<ApplicationUser> Residents { get; set; } = null!;
    }
}