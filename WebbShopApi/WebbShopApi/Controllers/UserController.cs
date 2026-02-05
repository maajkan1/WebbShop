using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using WebbShopApi.DTOs;
using WebbShopApi.Models;
using WebbShopApi.Services;
using WebbShopApi.Services.Interfaces;

namespace WebbShopApi.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
   
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserById(id);
        
        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newUser = await _userService.RegisterUser(dto);

        return CreatedAtAction(
            nameof(GetUserById),
            new { id = newUser.Id }, 
            newUser                
        );

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
       var result = await _userService.Login(dto);

       return Ok(new
       {
           token = result.Token,
           username = result.Username,
           userId = result.UserId
       });
    }
    
    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateProfileAsync(GetUserDto dto)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized("Invalid user ID in token.");
        }
        
        try
        {
            var result = await _userService.UpdateUser(userId, dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}