using Microsoft.EntityFrameworkCore;
using Pizza_Ordaring.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordaring.Infrastructure.Context
{
    public class Pizza_OrdaringDbContext :DbContext
    {
        private static Pizza_OrdaringDbContext _instance;
        public static Pizza_OrdaringDbContext Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = new Pizza_OrdaringDbContext((DbContextOptions<Pizza_OrdaringDbContext>)new DbContextOptionsBuilder().UseSqlServer(ConfigurationManager.ConnectionStrings["PizzaDb"].ConnectionString).Options);
                return _instance;
            }
        }
        public Pizza_OrdaringDbContext(DbContextOptions<Pizza_OrdaringDbContext>options) :base(options)
        {
                
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<ExtraItem> ExtraItems { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
