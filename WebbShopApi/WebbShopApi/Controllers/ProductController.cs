using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebbShopApi.DTOs;
using WebbShopApi.Services.Interfaces;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
    {

        var allProducts = await _productService.GetAllProductsAsync(cancellationToken);
        return Ok(allProducts);
    }
    
    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        
       var productById = await _productService.GetProductByIdAsync(productId);

       return Ok(productById);
    }
    
    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetProductsByCategory(int categoryId)
    {
        
       var category = await _productService.GetAllProductsByCategoryAsync(categoryId);

       return Ok(category);
    }
    [Authorize]
    [HttpPost("{productId}/add-to-cart")]
    public async Task<IActionResult> AddToCart(int productId, [FromBody] AddCartItemDto addCartItemDto)
    {
        
        if (!User.Identity?.IsAuthenticated ?? true)
            return Unauthorized("You need to be logged in to add items to the cart.");
        
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized("Invalid user ID in token.");
        }
        
        if (addCartItemDto.Quantity <= 0)
            return BadRequest("Quantity must be greater than 0.");
        
        var addedItem = await _productService.AddToCartAsync(
            productId,
            addCartItemDto.Quantity,
            userId);
        return Ok(addedItem);
    }
}