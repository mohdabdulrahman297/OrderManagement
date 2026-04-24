using Order.ApplicationCore.Entities;

namespace Order.ApplicationCore.Contracts.Repository;

public interface IOrderRepository
{
    Task<IEnumerable<Entities.Order>> GetAllOrdersAsync();
    Task<Entities.Order?> GetOrderByIdAsync(int id);
    Task<IEnumerable<Entities.Order>> GetOrdersByCustomerIdAsync(int customerId);
    Task<Entities.Order> AddOrderAsync(Entities.Order order);
    Task<Entities.Order> UpdateOrderAsync(Entities.Order order);
    Task<bool> DeleteOrderAsync(int id);
}