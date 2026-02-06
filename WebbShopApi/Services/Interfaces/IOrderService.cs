using WebbShopApi.DTOs;

namespace WebbShopApi.Services.Interfaces;

public interface IOrderService
{
    Task<int> CreateOrderAsync(Guid userId);
    Task<List<OrderDto>> GetOrderHistoryAsync(Guid userId);
    Task<decimal> GetTotalOrdersValueAsync(Guid userId);
}