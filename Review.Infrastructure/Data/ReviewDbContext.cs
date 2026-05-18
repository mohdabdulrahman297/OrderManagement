using Microsoft.EntityFrameworkCore;
using Review.ApplicationCore.Entities;

namespace Review.Infrastructure.Data
{
    public class ReviewDbContext : DbContext
    {
        public ReviewDbContext(DbContextOptions<ReviewDbContext> options) : base(options) { }

        public DbSet<CustomerReview> Reviews => Set<CustomerReview>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerReview>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.CustomerName).HasMaxLength(100).IsRequired();
                entity.Property(r => r.Comment).HasMaxLength(1000).IsRequired();
                entity.Property(r => r.Rating).IsRequired();
                entity.Property(r => r.IsApproved).HasDefaultValue(false);
                entity.Property(r => r.CreatedAt).IsRequired();
            });
        }
    }
}