using Microsoft.EntityFrameworkCore;
using Order.ApplicationCore.Contracts.Repository;
using Order.ApplicationCore.Entities;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly OrderDbContext _context;

    public OrderDetailRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        => await _context.OrderDetails
            .AsNoTracking()
            .ToListAsync();

    public async Task<OrderDetail?> GetOrderDetailByIdAsync(int id)
        => await _context.OrderDetails
            .AsNoTracking()
            .FirstOrDefaultAsync(od => od.Id == id);

    public async Task<IEnumerable<OrderDetail>> GetDetailsByOrderIdAsync(int orderId)
        => await _context.OrderDetails
            .Where(od => od.OrderId == orderId)
            .AsNoTracking()
            .ToListAsync();

    public async Task<OrderDetail> AddOrderDetailAsync(OrderDetail orderDetail)
    {
        _context.OrderDetails.Add(orderDetail);
        await _context.SaveChangesAsync();
        return orderDetail;
    }

    public async Task<OrderDetail> UpdateOrderDetailAsync(OrderDetail orderDetail)
    {
        _context.OrderDetails.Update(orderDetail);
        await _context.SaveChangesAsync();
        return orderDetail;
    }

    public async Task<bool> DeleteOrderDetailAsync(int id)
    {
        var detail = await _context.OrderDetails.FindAsync(id);
        if (detail is null) return false;

        _context.OrderDetails.Remove(detail);
        await _context.SaveChangesAsync();
        return true;
    }
}