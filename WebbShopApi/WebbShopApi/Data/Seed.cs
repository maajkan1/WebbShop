using WebbShopApi.Models;

namespace WebbShopApi.Data;

public class Seed
{
public static async Task SeedAsync(WebbShopDbContext db)
    {
        // =========================
        // USERS + CARTS
        // =========================
        if (!db.Users.Any())
        {
            var admin = new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Email = "admin@webbshop.se",
                FirstName = "Admin",
                LastName = "User",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                CreatedAt = DateTime.UtcNow
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "user",
                Email = "user@webbshop.se",
                FirstName = "Regular",
                LastName = "User",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("User123!"),
                CreatedAt = DateTime.UtcNow
            };

            db.Users.AddRange(admin, user);
            await db.SaveChangesAsync();

            db.Carts.AddRange(
                new Cart { UserId = admin.Id },
                new Cart { UserId = user.Id }
            );

            await db.SaveChangesAsync();
        }

        // =========================
        // CATEGORIES
        // =========================
        if (!db.Categories.Any())
        {
            db.Categories.AddRange(
                new Category
                {
                    CategoryName = "Kläder",
                    CategoryDescription = "Alla typer av kläder"
                },
                new Category
                {
                    CategoryName = "Accessoarer",
                    CategoryDescription = "Kepsar, väskor m.m."
                },
                new Category
                {
                    CategoryName = "Skor",
                    CategoryDescription = "Sneakers och träningsskor"
                },
                new Category
                {
                    CategoryName = "Träning",
                    CategoryDescription = "Produkter för träning och hälsa"
                },
                new Category
                {
                    CategoryName = "Elektronik",
                    CategoryDescription = "Teknik och tillbehör"
                }
            );

            await db.SaveChangesAsync();
        }

        // =========================
        // PRODUCTS + PRODUCT CATEGORIES
        // =========================
        if (!db.Products.Any())
        {
            var tshirt = new Product
            {
                ProductName = "T-shirt",
                ProductDescription = "Svart T-shirt i bomull",
                ProductPrice = 199,
                Quantity = 100
            };

            var hoodie = new Product
            {
                ProductName = "Hoodie",
                ProductDescription = "Grå hoodie med tryck",
                ProductPrice = 499,
                Quantity = 50
            };

            var cap = new Product
            {
                ProductName = "Keps",
                ProductDescription = "Svart keps med broderad logga",
                ProductPrice = 249,
                Quantity = 75
            };

            var sneakers = new Product
            {
                ProductName = "Sneakers",
                ProductDescription = "Vita sneakers för vardagsbruk",
                ProductPrice = 899,
                Quantity = 40
            };

            var runningShoes = new Product
            {
                ProductName = "Löparskor",
                ProductDescription = "Lätta löparskor med bra dämpning",
                ProductPrice = 1299,
                Quantity = 30
            };

            var yogaMat = new Product
            {
                ProductName = "Yogamatta",
                ProductDescription = "Halkfri yogamatta i naturgummi",
                ProductPrice = 349,
                Quantity = 60
            };

            var dumbbells = new Product
            {
                ProductName = "Hantlar 2x10kg",
                ProductDescription = "Justerbara hantlar för hemmaträning",
                ProductPrice = 799,
                Quantity = 25
            };

            var headphones = new Product
            {
                ProductName = "Trådlösa hörlurar",
                ProductDescription = "Bluetooth-hörlurar med brusreducering",
                ProductPrice = 1499,
                Quantity = 35
            };

            db.Products.AddRange(
                tshirt, hoodie, cap,
                sneakers, runningShoes,
                yogaMat, dumbbells,
                headphones
            );

            await db.SaveChangesAsync();

            var clothes = db.Categories.First(c => c.CategoryName == "Kläder");
            var accessories = db.Categories.First(c => c.CategoryName == "Accessoarer");
            var shoes = db.Categories.First(c => c.CategoryName == "Skor");
            var training = db.Categories.First(c => c.CategoryName == "Träning");
            var electronics = db.Categories.First(c => c.CategoryName == "Elektronik");

            db.ProductCategories.AddRange(
                new ProductCategory { ProductId = tshirt.Id, CategoryId = clothes.Id },
                new ProductCategory { ProductId = hoodie.Id, CategoryId = clothes.Id },

                new ProductCategory { ProductId = cap.Id, CategoryId = accessories.Id },

                new ProductCategory { ProductId = sneakers.Id, CategoryId = shoes.Id },
                new ProductCategory { ProductId = runningShoes.Id, CategoryId = shoes.Id },

                new ProductCategory { ProductId = yogaMat.Id, CategoryId = training.Id },
                new ProductCategory { ProductId = dumbbells.Id, CategoryId = training.Id },

                new ProductCategory { ProductId = headphones.Id, CategoryId = electronics.Id }
            );

            await db.SaveChangesAsync();
        }

        // =========================
        // CART ITEMS (ACTIVE CART)
        // =========================
        if (!db.CartItems.Any())
        {
            var user = db.Users.First(u => u.Email == "user@webbshop.se");
            var cart = db.Carts.First(c => c.UserId == user.Id);

            var tshirt = db.Products.First(p => p.ProductName == "T-shirt");
            var cap = db.Products.First(p => p.ProductName == "Keps");

            db.CartItems.AddRange(
                new CartItem
                {
                    CartId = cart.Id,
                    ProductId = tshirt.Id,
                    Quantity = 2
                },
                new CartItem
                {
                    CartId = cart.Id,
                    ProductId = cap.Id,
                    Quantity = 1
                }
            );

            await db.SaveChangesAsync();
        }

        // =========================
        // ORDERS + ORDER ITEMS
        // =========================
        if (!db.Orders.Any())
        {
            var user = db.Users.First(u => u.Email == "user@webbshop.se");

            var hoodie = db.Products.First(p => p.ProductName == "Hoodie");
            var sneakers = db.Products.First(p => p.ProductName == "Sneakers");

            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.UtcNow
            };

            db.Orders.Add(order);
            await db.SaveChangesAsync();

            db.OrderItems.AddRange(
                new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = hoodie.Id,
                    ProductName = hoodie.ProductName,
                    Quantity = 1,
                    UnitPrice = hoodie.ProductPrice
                },
                new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = sneakers.Id,
                    ProductName = sneakers.ProductName,
                    Quantity = 1,
                    UnitPrice = sneakers.ProductPrice
                }
            );

            await db.SaveChangesAsync();
        }
    }
}