using Order.ApplicationCore.Contracts.Repository;
using Order.ApplicationCore.Contracts.Services;
using OrderEntity = Order.ApplicationCore.Entities.Order;

namespace Order.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<IEnumerable<OrderEntity>> GetAllOrdersAsync()
        => _orderRepository.GetAllOrdersAsync();

    public Task<OrderEntity?> GetOrderByIdAsync(int id)
        => _orderRepository.GetOrderByIdAsync(id);

    public Task<IEnumerable<OrderEntity>> GetOrdersByCustomerIdAsync(int customerId)
        => _orderRepository.GetOrdersByCustomerIdAsync(customerId);

    public Task<OrderEntity> CreateOrderAsync(OrderEntity order)
        => _orderRepository.AddOrderAsync(order);

    public Task<OrderEntity> UpdateOrderAsync(OrderEntity order)
        => _orderRepository.UpdateOrderAsync(order);

    public Task<bool> DeleteOrderAsync(int id)
        => _orderRepository.DeleteOrderAsync(id);
}