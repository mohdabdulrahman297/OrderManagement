using Microsoft.EntityFrameworkCore;
using Order.ApplicationCore.Contracts.Repository;
using Order.Infrastructure.Data;
using OrderEntity = Order.ApplicationCore.Entities.Order;

namespace Order.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    // Get all orders including their details
    public async Task<IEnumerable<OrderEntity>> GetAllOrdersAsync()
        => await _context.Orders
            .Include(o => o.OrderDetails)
            .AsNoTracking()
            .ToListAsync();

    // Get one order by its Id
    public async Task<OrderEntity?> GetOrderByIdAsync(int id)
        => await _context.Orders
            .Include(o => o.OrderDetails)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);

    // Get all orders for a specific customer
    public async Task<IEnumerable<OrderEntity>> GetOrdersByCustomerIdAsync(int customerId)
        => await _context.Orders
            .Include(o => o.OrderDetails)
            .Where(o => o.CustomerId == customerId)
            .AsNoTracking()
            .ToListAsync();

    // Add a new order
    public async Task<OrderEntity> AddOrderAsync(OrderEntity order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    // Update an existing order
    public async Task<OrderEntity> UpdateOrderAsync(OrderEntity order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }

    // Delete an order by Id
    public async Task<bool> DeleteOrderAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null) return false;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }
}