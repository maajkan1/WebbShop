namespace WebbShopApi.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } 
    public Guid UserId { get; set; }
    public User User { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    public OrderItem OrderItem { get; set; }
}