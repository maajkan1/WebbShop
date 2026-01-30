using Microsoft.EntityFrameworkCore;
using WebbShopApi.DTOs;
using WebbShopApi.Models;
using WebbShopApi.Services.Interfaces;

namespace WebbShopApi.Services;

public class CartService : ICartService
{
    private readonly WebbShopDbContext _context;
    private readonly ILogger<CartService> _logger;

    public CartService(WebbShopDbContext context, ILogger<CartService> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<List<CartItemDto>> GetCartItemsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.CartItems
            .AsNoTracking()
            .Where(s => s.Cart.UserId == userId)
            .Select(c => new CartItemDto
            {   
                Id = c.Id,
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                Product = new ProductDto
                {
                    Id = c.Product.Id,
                    ProductName = c.Product.ProductName,
                    ProductDescription = c.Product.ProductDescription,
                    ProductPrice = c.Product.ProductPrice
                }
            })
            .ToListAsync(cancellationToken);
    }

    public async Task RemoveCartItemAsync(Guid userId, int cartItemId)
    {
        var cartItem = await _context.CartItems
            .Include(ci => ci.Cart)
            .FirstOrDefaultAsync(c => c.Id == cartItemId && c.Cart.UserId == userId);

        if (cartItem == null)
            throw new InvalidOperationException("Cart item not found or does not belong to the user.");

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveWholeCartAsync(Guid userId)
    {
        var cart = await _context.CartItems.FirstOrDefaultAsync(ci => ci.Cart.UserId == userId);

        if (cart == null)
        {
            throw new InvalidOperationException("Cart item not found or does not belong to the user.");
        }
        
        _context.CartItems.Remove(cart);
        await _context.SaveChangesAsync();
    }
}