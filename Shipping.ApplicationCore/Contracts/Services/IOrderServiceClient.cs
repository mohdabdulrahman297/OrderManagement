using Shipping.ApplicationCore.DTOs;

namespace Shipping.ApplicationCore.Contracts.Services
{
    public interface IOrderServiceClient
    {
        Task<bool> UpdateShippingStatusAsync(ShippingStatusUpdateDto dto);
    }
}