using AuthService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite primary key for junction table
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // UserRole → User
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            // UserRole → Role
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Unique username and email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.EmailId).IsUnique();

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Full access to all features" },
                new Role { Id = 2, Name = "Customer", Description = "Can view and edit own profile" },
                new Role { Id = 3, Name = "Guest", Description = "Limited access, browse products only" }
            );
        }
    }
}