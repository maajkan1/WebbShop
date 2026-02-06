using System.ComponentModel.DataAnnotations;

namespace WebbShopApi.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    [Required(ErrorMessage = "Lösenord krävs")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Lösenordet måste vara minst 8 tecken")]
    [DataType(DataType.Password)]
    public string PasswordHash { get; set; }
    public Cart ActiveCart { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}