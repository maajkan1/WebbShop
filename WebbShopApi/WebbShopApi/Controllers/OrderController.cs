using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebbShopApi.Services.Interfaces;

namespace WebbShopApi.Controllers;
[ApiController]
[Route("orders")]

public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    private bool TryGetUserId(out Guid userId)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(userIdClaim, out userId);
    }
    
    [Authorize]
    [HttpPost("create-order/")]
    public async Task<IActionResult> CreateOrder()
    {
        
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized("Invalid user ID in token.");
        }
        
        var order = await _orderService.CreateOrderAsync(userId);

        return CreatedAtAction(nameof(GetOrderHistory), order);
    }

    [Authorize]
    [HttpGet("order-history")]
    public async Task<IActionResult> GetOrderHistory()
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized("Invalid user ID in token.");
        }
        
        var orderHistory = await _orderService.GetOrderHistoryAsync(userId);
        return Ok(orderHistory);
    }

    [Authorize]
    [HttpGet("order-history/value")]
    public async Task<IActionResult> GetTotalOrdersValue()
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized("Invalid user ID in token.");
        }

        var orderValue = await _orderService.GetTotalOrdersValueAsync(userId);
            
            return Ok(orderValue);
    }
}