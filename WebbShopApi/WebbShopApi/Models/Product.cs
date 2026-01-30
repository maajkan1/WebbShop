using System.ComponentModel.DataAnnotations;

namespace WebbShopApi.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    [Range(0, 999)]
    public int Quantity { get; set; }

    public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    public ProductCategory ProductCategory { get; set; }
}