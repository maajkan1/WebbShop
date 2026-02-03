using WebbShopApi.DTOs;
using WebbShopApi.Models;

namespace WebbShopApi.Services.Interfaces;

public interface IUserService
{
     Task<GetUserDto> RegisterUser(RegisterDto dto);
     Task<GetUserDto> GetUserById(Guid userId);
     Task<LoginResult> Login(LoginDto dto);
}