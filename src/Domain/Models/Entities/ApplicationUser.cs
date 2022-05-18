using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Guid? RoomId { get; set; }
        public virtual Room Room { get; set; } = null!;
        public virtual ICollection<Bill> Bills { get; set; } = null!;
    }
}