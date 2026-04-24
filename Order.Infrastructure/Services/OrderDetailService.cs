using Order.ApplicationCore.Contracts.Repository;
using Order.ApplicationCore.Contracts.Services;
using Order.ApplicationCore.Entities;

namespace Order.Infrastructure.Services;

public class OrderDetailService : IOrderDetailService
{
    private readonly IOrderDetailRepository _detailRepository;

    public OrderDetailService(IOrderDetailRepository detailRepository)
    {
        _detailRepository = detailRepository;
    }

    public Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        => _detailRepository.GetAllOrderDetailsAsync();

    public Task<OrderDetail?> GetOrderDetailByIdAsync(int id)
        => _detailRepository.GetOrderDetailByIdAsync(id);

    public Task<IEnumerable<OrderDetail>> GetDetailsByOrderIdAsync(int orderId)
        => _detailRepository.GetDetailsByOrderIdAsync(orderId);

    public Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail)
        => _detailRepository.AddOrderDetailAsync(orderDetail);

    public Task<OrderDetail> UpdateOrderDetailAsync(OrderDetail orderDetail)
        => _detailRepository.UpdateOrderDetailAsync(orderDetail);

    public Task<bool> DeleteOrderDetailAsync(int id)
        => _detailRepository.DeleteOrderDetailAsync(id);
}