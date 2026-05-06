using Microsoft.EntityFrameworkCore;
using Product.ApplicationCore.Entities;
using System.Reflection.Emit;
using ProductEntity = Product.ApplicationCore.Entities.Product;

namespace Product.Infrastructure.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<ProductDetail> ProductDetails => Set<ProductDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductEntity>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).IsRequired().HasMaxLength(100);
            e.Property(p => p.Description).HasMaxLength(500);
            e.Property(p => p.Category).HasMaxLength(100);
            e.Property(p => p.SKU).HasMaxLength(50);
            e.Property(p => p.Price).HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<ProductDetail>(e =>
        {
            e.HasKey(pd => pd.Id);
            e.Property(pd => pd.AttributeName).IsRequired().HasMaxLength(100);
            e.Property(pd => pd.AttributeValue).HasMaxLength(200);
            e.Property(pd => pd.Unit).HasMaxLength(50);

            e.HasOne(pd => pd.Product)
             .WithMany(p => p.ProductDetails)
             .HasForeignKey(pd => pd.ProductId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}