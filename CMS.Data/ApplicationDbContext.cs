
using CMS.Data.Entities;
using CosmeticsAPI.Settings;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CategoriesProduct> CategoriesProduct { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<CustomerAddress> CustomerAddresses { get; set; }


        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }






        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            // ================================
            // PRODUCT - CATEGORY PRODUCT
            // ================================

            builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryProductId)
                .OnDelete(DeleteBehavior.Restrict);


            // ================================
            // DECIMAL PRICE
            // ================================

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);


        }

    }
}