using Microsoft.EntityFrameworkCore;
using WebbShopApi.DTOs;
using WebbShopApi.Models;
using WebbShopApi.Services.Interfaces;

namespace WebbShopApi.Services;

public class ProductService : IProductService
{
    private readonly WebbShopDbContext _context;
    private readonly ILogger<CartService> _logger;

    public ProductService(WebbShopDbContext context, ILogger<CartService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        var allProducts = await _context.Products
            .AsNoTracking()
            .OrderBy(x => x.ProductName)
            .Select(product => new ProductDto
            {
                Id = product.Id,
                ProductDescription = product.ProductDescription,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                Quantity = product.Quantity
            })
            .ToListAsync(cancellationToken);

        if (allProducts.Count == 0)
        {
            throw new Exception("Products not found");
        }
        
        return allProducts;
    }


    public async Task<List<ProductDto>> GetAllProductsByCategoryAsync(int categoryId)
    {
        var getProductsByCategory = await _context.Products
            .AsNoTracking()
            .Where(p => p.ProductCategories.Any(c => c.CategoryId == categoryId))
            .Select(dto => new ProductDto
            {
                Id = dto.Id,
                ProductDescription = dto.ProductDescription,
                ProductName = dto.ProductName,
                ProductPrice = dto.ProductPrice,
                Quantity = dto.Quantity
            }).ToListAsync();
        if (getProductsByCategory.Count == 0)
        {
            throw new Exception("Products not found");
        }

        return getProductsByCategory;
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var getProductById = await _context.Products
            .AsNoTracking()
            .Select(dto => new ProductDto
            {
                Id = dto.Id,
                ProductDescription = dto.ProductDescription,
                ProductName = dto.ProductName,
                ProductPrice = dto.ProductPrice,
                Quantity = dto.Quantity
            })
            .FirstOrDefaultAsync(p => p.Id == id);

        if (getProductById == null)
        {
            throw new Exception("Product not found");
        }
        
        return getProductById;

    }

    public async Task<AddCartItemDto> AddToCartAsync(int productId, int quantity, Guid userId)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var cart = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci =>
                    ci.CartId == cart.Id && ci.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = product.Id,
                    Quantity = quantity
                };

                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return new AddCartItemDto
            {
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = cartItem.Quantity
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

}
