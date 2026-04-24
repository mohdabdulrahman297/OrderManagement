using Microsoft.EntityFrameworkCore;
using Order.ApplicationCore.Entities;          // ← must have this
using OrderEntity = Order.ApplicationCore.Entities.Order;  // ← add this alias

namespace Order.Infrastructure.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<OrderEntity> Orders => Set<OrderEntity>();        // use alias
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderEntity>(e =>        // use alias
        {
            e.HasKey(o => o.Id);
            e.Property(o => o.CustomerName).IsRequired().HasMaxLength(100);
            e.Property(o => o.ShippingAddress).HasMaxLength(200);
            e.Property(o => o.ShippingMethod).HasMaxLength(50);
            e.Property(o => o.PaymentName).HasMaxLength(50);
            e.Property(o => o.OrderStatus).HasMaxLength(50);
            e.Property(o => o.BillAmount).HasColumnType("decimal(10,2)");
        });

        modelBuilder.Entity<OrderDetail>(e =>
        {
            e.HasKey(od => od.Id);
            e.Property(od => od.ProductName).IsRequired().HasMaxLength(100);
            e.Property(od => od.Price).HasColumnType("decimal(10,2)");

            e.HasOne(od => od.Order)
             .WithMany(o => o.OrderDetails)
             .HasForeignKey(od => od.OrderId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}