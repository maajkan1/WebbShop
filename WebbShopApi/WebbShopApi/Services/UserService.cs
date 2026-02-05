using Microsoft.EntityFrameworkCore;
using WebbShopApi.DTOs;
using WebbShopApi.Models;
using WebbShopApi.Services.Interfaces;

namespace WebbShopApi.Services;

public class UserService : IUserService
{
    private readonly JwtService _jwtService;
    private readonly WebbShopDbContext _context;
    private readonly  ILogger<UserService> _logger;
    public UserService(WebbShopDbContext context, ILogger<UserService> logger, JwtService jwtService)
    {
        _context = context;
        _logger = logger;
        _jwtService = jwtService;
    }
    public async Task<GetUserDto> RegisterUser(RegisterDto dto)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        
        if (existingUser != null)
        {
            throw new Exception("User already exist");
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
        
        _logger.LogInformation("Registered new user: {Username}", newUser.Username);

        var sendUserInfo = new GetUserDto
        {
            Id = newUser.Id,
            Username = newUser.Username,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Email = newUser.Email,
            CreatedAt = newUser.CreatedAt
        };
        
        return sendUserInfo;
    }

    public async Task<GetUserDto> GetUserById(Guid userId)
    {
        var specificUser = await _context.Users
            .AsAsyncEnumerable()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (specificUser == null)
        {
            _logger.LogWarning("User {UserId} not found", userId);
            throw new Exception("User not found");
        }
        _logger.LogInformation("Fetched user with ID {UserId}", userId);

        var userDto = new GetUserDto
        {
            Id = specificUser.Id,
            Username = specificUser.Username,
            FirstName = specificUser.FirstName,
            LastName = specificUser.LastName,
            Email = specificUser.Email,
            CreatedAt = specificUser.CreatedAt
        };
        return userDto;
    }

    public async Task<LoginResult> Login(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null)
        {
            throw new Exception("Invalid input");
        }

        bool validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

        if (!validPassword)
        {
            throw new Exception("Invalid input");
        }
        
        var token =  _jwtService.GenerateToken(user);
        
        return new LoginResult
        {
            Token = token,
            Username = user.Username,
            UserId = user.Id
        };
       
    }

    public async Task<GetUserDto> UpdateUser(Guid userId, GetUserDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated user {UserId}", userId);
        return new GetUserDto
        {
            Id = user.Id,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }
}