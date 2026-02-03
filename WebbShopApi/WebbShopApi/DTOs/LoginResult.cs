namespace WebbShopApi.DTOs;

public class LoginResult
{
    public string Token { get; set; }
    public string Username { get; set; }
    public Guid UserId { get; set; }
}
