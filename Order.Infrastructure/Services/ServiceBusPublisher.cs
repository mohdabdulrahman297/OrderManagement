using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Order.ApplicationCore.Contracts.Services;
using Order.ApplicationCore.Events;
using System.Text.Json;

namespace Order.Infrastructure.Services
{
    public class ServiceBusPublisher : IMessagePublisher
    {
        private readonly string _connectionString;
        private readonly string _queueName;

        public ServiceBusPublisher(IConfiguration config)
        {
            _connectionString = config["AzureServiceBus:ConnectionString"]!;
            _queueName = config["AzureServiceBus:OrderCompletedQueue"]!;
        }

        public async Task PublishOrderCompletedAsync(OrderCompletedEvent orderEvent)
        {
            await using var client = new ServiceBusClient(_connectionString);
            var sender = client.CreateSender(_queueName);

            var json = JsonSerializer.Serialize(orderEvent);
            var message = new ServiceBusMessage(json)
            {
                Subject = "OrderCompleted",
                ContentType = "application/json"
            };

            await sender.SendMessageAsync(message);
            Console.WriteLine($"[ServiceBus] Published OrderCompleted for OrderId: {orderEvent.OrderId}");
        }
    }
}