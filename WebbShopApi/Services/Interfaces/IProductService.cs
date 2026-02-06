using WebbShopApi.DTOs;

namespace WebbShopApi.Services.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken);
    
    Task<List<ProductDto>> GetAllProductsByCategoryAsync(int categoryId);
    
    Task<ProductDto> GetProductByIdAsync(int id);
    
    Task<AddCartItemDto> AddToCartAsync(int id, int quantity, Guid userId);
}