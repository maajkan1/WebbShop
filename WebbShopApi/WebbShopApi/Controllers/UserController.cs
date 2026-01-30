using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using WebbShopApi.DTOs;
using WebbShopApi.Models;

namespace WebbShopApi.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly WebbShopDbContext _context;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, WebbShopDbContext context, JwtService jwtService)
    {
        _logger = logger;
        _context = context;
        _jwtService = jwtService;
    }
    /*
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var allUsers = await _context.Users
            .Select(u => new GetUserDto
            {
                Id = u.Id,
                Nickname = u.Nickname
            })
            .ToListAsync();
        _logger.LogInformation("Fetching all users");
        
        return Ok(allUsers);
    }
    */
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var specificUser = await _context.Users
            .FindAsync(id);

        if (specificUser == null)
        {
            _logger.LogWarning("User {UserId} not found", id);
            return NotFound(new { message = "User not found"});
        }
        _logger.LogInformation("Fetched user with ID {UserId}", id);

        var userDto = new GetUserDto
        {
            Id = specificUser.Id,
            Username = specificUser.Username,
            FirstName = specificUser.FirstName,
            LastName = specificUser.LastName,
            Email = specificUser.Email,
            CreatedAt = specificUser.CreatedAt
        };
        return Ok(userDto);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        
        if (existingUser != null)
        {
            return Conflict(new { message = $"Username {dto.Username} already exists" });
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var newUser = new User
        {
            Username = dto.Username,
            PasswordHash = hashedPassword,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow
            
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(
            nameof(GetUserById), 
            new { id = newUser.Id }, 
            new GetUserDto                    
            {
                Id = newUser.Id,
                Username = newUser.Username,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                CreatedAt = newUser.CreatedAt
            });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null)
        {
            return Unauthorized(new { message = "Invalid input" });
        }

        bool validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!validPassword)
        {
            return Unauthorized(new { message = "Invalid input" });
        }
        
        var token =  _jwtService.GenerateToken(user);
        return Ok(new 
        { 
            token, 
            username = user.Username, 
            userId = user.Id 
        });
    }
}