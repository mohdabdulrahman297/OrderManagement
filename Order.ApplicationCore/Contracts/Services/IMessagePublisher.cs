using Order.ApplicationCore.Events;

namespace Order.ApplicationCore.Contracts.Services
{
    public interface IMessagePublisher
    {
        Task PublishOrderCompletedAsync(OrderCompletedEvent orderEvent);
    }
}