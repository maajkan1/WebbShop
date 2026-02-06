using Microsoft.EntityFrameworkCore;
using WebbShopApi.DTOs;
using WebbShopApi.Models;
using WebbShopApi.Services.Interfaces;

namespace WebbShopApi.Services;

public class OrderService : IOrderService
{
    private readonly WebbShopDbContext _context;

    public OrderService(WebbShopDbContext context)
    {
        _context = context;
    }
    public async Task<int> CreateOrderAsync(Guid userId)
    {
        
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null || !cart.CartItems.Any())
            throw new InvalidOperationException("Cart is empty");

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var ci in cart.CartItems)
            {
                
                if (ci.Product.Quantity < ci.Quantity) 
                {
                    throw new Exception("Ordered to many products");
                }
                
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.ProductName,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Product.ProductPrice
                };
                _context.OrderItems.Add(orderItem);
                
                ci.Product.Quantity -= ci.Quantity;
            }
            
            _context.CartItems.RemoveRange(cart.CartItems);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return order.Id;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<OrderDto>> GetOrderHistoryAsync(Guid userId)
    {
        var getOrders = _context.Orders
            .AsNoTracking()
            .Where(o => o.UserId == userId)
            .Select(s => new OrderDto
            {
                Id = s.Id,
                OrderDate = s.OrderDate,
                Items = s.Items.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.ProductName,
                    Price = oi.UnitPrice,
                    Quantity = oi.Quantity,
                }).ToList()
                
            })
            .ToListAsync();
        
        return await getOrders;
    }

    public async Task<decimal> GetTotalOrdersValueAsync(Guid userId)
    {
        return await _context.OrderItems
            .AsNoTracking()
            .Where(oi => oi.Order.UserId == userId)
            .SumAsync(oi => (decimal?)oi.Quantity * oi.UnitPrice) ?? 0;
    }
}