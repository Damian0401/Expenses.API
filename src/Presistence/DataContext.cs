using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Presistence
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Module> Modules { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasKey(x => x.Id); 
            builder.Entity<ApplicationUser>()
                .Property(x => x.FirstName)
                .HasMaxLength(255);
            builder.Entity<ApplicationUser>()
                .Property(x => x.LastName)
                .HasMaxLength(255);
            builder.Entity<ApplicationUser>()
                .HasOne(x => x.Room)
                .WithMany(x => x.Residents)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.Entity<Bill>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Bills)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Bill>()
                .Property(x => x.Description)
                .HasMaxLength(255);

            builder.Entity<Room>()
                .HasOne(x => x.Module)
                .WithMany(x => x.Rooms)
                .HasForeignKey(x => x.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Module>()
                .Property(x => x.Name)
                .HasMaxLength(255);

            var converter = new EnumToStringConverter<Status>();

            builder.Entity<Bill>()
                .Property(x => x.Status)
                .HasConversion(converter);
        }
    }
}