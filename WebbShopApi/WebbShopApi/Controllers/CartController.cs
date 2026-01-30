using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebbShopApi.Services.Interfaces;

[Authorize] 
[ApiController]
[Route("cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("items")]
    public async Task<IActionResult> GetAllCartItems()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized("Invalid user ID in token.");
        }

        var cartItems = await _cartService.GetCartItemsAsync(userId);
        return Ok(cartItems);
    }
    
    [HttpDelete("items/{cartItemId}")]
    public async Task<IActionResult> RemoveCartItem(int cartItemId)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized("Invalid user ID in token.");
        }
        
        await _cartService.RemoveCartItemAsync(userId, cartItemId);

        return NoContent();
    }
    
    [HttpDelete("items/")]
    public async Task<IActionResult> RemoveWholeCart()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized("Invalid user ID in token.");
        }
        
        await _cartService.RemoveWholeCartAsync(userId);

        return NoContent();
    }
}