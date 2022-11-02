using ECommerce.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data.Data
{
    public class ECommerceDBContext : IdentityDbContext
    {
        public ECommerceDBContext() { }
        public ECommerceDBContext(DbContextOptions<ECommerceDBContext> options) : base(options) { }
        public DbSet<Product> products { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartDetail> cartDetails { get; set; }

        public DbSet<Category> categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Get connection to local server (Instead of getting connection at Program.cs)
            // But it still needs to be setted AddDbContext
            optionsBuilder.UseSqlServer("Server=.;Database=Ecommerce;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
