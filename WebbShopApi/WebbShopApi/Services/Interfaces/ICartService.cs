using WebbShopApi.DTOs;
using WebbShopApi.Models;

namespace WebbShopApi.Services.Interfaces;

public interface ICartService
{
    Task<List<CartItemDto>> GetCartItemsAsync(Guid userId, CancellationToken cancellationToken = default);
    
    Task RemoveCartItemAsync(Guid userId, int cartItemId);
    
    Task RemoveWholeCartAsync(Guid userId);
    
    /*Task<List<Cart>> GetCartHistory(Guid userId);*/ // Kanske för admin?
}