using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Promotion.ApplicationCore.Contracts.Services;
using Promotion.ApplicationCore.Events;
using System.Text.Json;

namespace Promotion.Infrastructure.Services
{
    public class PromotionServiceBusPublisher : IPromotionPublisher
    {
        private readonly string _connectionString;
        private readonly string _startedQueue;
        private readonly string _endedQueue;

        public PromotionServiceBusPublisher(IConfiguration config)
        {
            _connectionString = config["AzureServiceBus:ConnectionString"]!;
            _startedQueue = config["AzureServiceBus:PromotionStartedQueue"]!;
            _endedQueue = config["AzureServiceBus:PromotionEndedQueue"]!;
        }

        public async Task PublishPromotionStartedAsync(PromotionStartedEvent promotionEvent)
        {
            await PublishMessageAsync(_startedQueue, promotionEvent, "PromotionStarted");
        }

        public async Task PublishPromotionEndedAsync(PromotionEndedEvent promotionEvent)
        {
            await PublishMessageAsync(_endedQueue, promotionEvent, "PromotionEnded");
        }

        private async Task PublishMessageAsync<T>(string queueName, T eventData, string subject)
        {
            await using var client = new ServiceBusClient(_connectionString);
            var sender = client.CreateSender(queueName);

            var json = JsonSerializer.Serialize(eventData);
            var message = new ServiceBusMessage(json)
            {
                Subject = subject,
                ContentType = "application/json"
            };

            await sender.SendMessageAsync(message);
            Console.WriteLine($"[ServiceBus] Published {subject} event");
        }
    }
}