using Order.ApplicationCore.Entities;

namespace Order.ApplicationCore.Contracts.Repository;

public interface IOrderDetailRepository
{
    Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
    Task<OrderDetail?> GetOrderDetailByIdAsync(int id);
    Task<IEnumerable<OrderDetail>> GetDetailsByOrderIdAsync(int orderId);
    Task<OrderDetail> AddOrderDetailAsync(OrderDetail orderDetail);
    Task<OrderDetail> UpdateOrderDetailAsync(OrderDetail orderDetail);
    Task<bool> DeleteOrderDetailAsync(int id);
}