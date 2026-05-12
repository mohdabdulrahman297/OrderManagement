using System.Text;
using System.Text.Json;
using Shipping.ApplicationCore.Contracts.Services;
using Shipping.ApplicationCore.DTOs;

namespace Shipping.Infrastructure.Services
{
    public class OrderServiceClient : IOrderServiceClient
    {
        private readonly HttpClient _httpClient;

        public OrderServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> UpdateShippingStatusAsync(ShippingStatusUpdateDto dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PatchAsync(
                $"api/Order/{dto.OrderId}/shipping-status", content);

            return response.IsSuccessStatusCode;
        }
    }
}