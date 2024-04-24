using Microsoft.EntityFrameworkCore;
using Pizza_OrderingAPI.Entities;

namespace Pizza_OrderingAPI.Data
{
    public class ShopOnlineDbContext : DbContext
    {
        public ShopOnlineDbContext(DbContextOptions<ShopOnlineDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductMenu>()
    .HasMany(p => p.Sizes) // One ProductMenus can have multiple PizzaSizes
    .WithOne(s => s.Pizza) // Each PizzaSize belongs to one ProductMenus
    .HasForeignKey(p => p.PizzaId);



         //   modelBuilder.Entity<ExtraItem>()
         //.HasOne(e => e.CartItem)         // Each ExtraItem belongs to one CartItem
         //.WithMany(c => c.ExtraItems)     // Each CartItem can have multiple ExtraItems
         //.HasForeignKey(e => e.CartItemId);  // Define the foreign key property


            // Seed data for City table
            modelBuilder.Entity<City>().HasData(
                new City { CityCode = "C1", CityName = "City 1" },
                new City { CityCode = "C2", CityName = "City 2" }
            // Add more cities as needed
            );

            // Seed data for Customer table
            modelBuilder.Entity<User>().HasData(
                new User { CustomerId = 1, Name = "John Doe", Email = "john@example.com", PhoneNumber = "123456789", Address = "123 Main St", CityCode = "C1" },
                new User { CustomerId = 2, Name = "Jane Smith", Email = "jane@example.com", PhoneNumber = "987654321", Address = "456 Elm St", CityCode = "C2" }
            // Add more customers as needed
            );

            // Seed data for OrderStatus table
            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { StatusId = 1, StatusName = "Pending" },
                new OrderStatus { StatusId = 2, StatusName = "Confirmed" },
                new OrderStatus { StatusId = 3, StatusName = "Delivered" },
                new OrderStatus { StatusId = 4, StatusName = "Cancelled" }
            // Add more order statuses as needed
            );
            modelBuilder.Entity<ItemCategory>().HasData(
      new ItemCategory { Id = 1, CategoryName = "Category 1" },
      new ItemCategory { Id = 2, CategoryName = "Category 2" }
  // Add more categories as needed
  );
            // Seed data for ProductMenus table
            modelBuilder.Entity<ProductMenu>().HasData(
                  new ProductMenu { PizzaId = 1, PizzaName = "Margherita", Description = "Tomato, cheese, basil" ,CategoryId=1,PizzaNum=1 },
                  new ProductMenu { PizzaId = 2, PizzaName = "Pepperoni", Description = "Pepperoni, cheese, tomato sauce" ,CategoryId=2,PizzaNum = 2}
              // Add more ProductMenus data as needed
              );

            // Seed PizzaSize data
            modelBuilder.Entity<ProductSize>().HasData(
                new ProductSize { SizeId = 1, Name = "Small", Price = 8.99m, PizzaId = 1 },
                new ProductSize { SizeId = 2, Name = "Medium", Price = 10.99m, PizzaId = 1 },
                new ProductSize { SizeId = 3, Name = "Large", Price = 12.99m, PizzaId = 1 },
                new ProductSize { SizeId = 4, Name = "Small", Price = 9.99m, PizzaId = 2 },
                new ProductSize { SizeId = 5, Name = "Medium", Price = 11.99m, PizzaId = 2 },
                new ProductSize { SizeId = 6, Name = "Large", Price = 13.99m, PizzaId = 2 }
            // Add more PizzaSize data as needed
            );

            // Seed data for OrderItem table
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, OrderId = 1, PizzaId = 1, Size = "Small", Quantity = 2, Subtotal = 17.98m },
                new OrderItem { OrderItemId = 2, OrderId = 1, PizzaId = 2, Size = "Medium", Quantity = 1, Subtotal = 11.99m }
            // Add more order items as needed
            );

            // Seed data for Order table
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderId = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    DeliveryAddress = "123 Main St",
                    TotalAmount = 29.97m, // Total amount of both items
                    StatusId = 1,
                    DesiredDeliveryTime = DateTime.Now.AddDays(1) // Example of setting desired delivery time
                }
            // Add more orders as needed
            );

            // Seed data for OrderConfirmation table
            modelBuilder.Entity<OrderConfirmation>().HasData(
                new OrderConfirmation { ConfirmationId = 1, OrderId = 1, ConfirmationDate = DateTime.Now, ConfirmationMessage = "Your order has been confirmed." }
            // Add more order confirmations as needed
            );

            // Seed data for CartItem table
            //modelBuilder.Entity<CartItem>().HasData(
            //    new CartItem { CartItemId = 1, ProductId = 1, Size = "Small", Quantity = 1 },
            //    new CartItem { CartItemId = 2, ProductId = 2, Size = "Medium", Quantity = 1 }
            //// Add more cart items as needed
            //);

            // Seed data for Cart table
            modelBuilder.Entity<Cart>().HasData(
           new Cart { UserId = 1, Id = 1 } // Example of a cart belonging to a user
                                            // Add more carts as needed
       );

            // Seed data for ExtraItem table
            modelBuilder.Entity<ExtraItem>().HasData(
                new ExtraItem { ExtraItemId = 1, Name = "Extra Cheese", Price = 1.50m },
                new ExtraItem { ExtraItemId = 2, Name = "Bacon", Price = 2.00m }
            // Add more extra items as needed
            );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ProductMenu> ProductMenus { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderConfirmation> OrderConfirmations { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ExtraItem> ExtraItems { get; set; }
        public DbSet<ProductSize> PizzaSizes { get; set; }
        
        public DbSet<ItemCategory> ItemCategory { get; set; }





    }
}
