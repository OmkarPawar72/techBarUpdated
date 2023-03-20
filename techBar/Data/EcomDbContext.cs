using Microsoft.EntityFrameworkCore;
using techBar.Models;

namespace techBar.Data
{
	public class EcomDbContext : DbContext
	{
		public EcomDbContext(DbContextOptions<EcomDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product_Category>().HasKey(pc => new
			{
				pc.ProductId,
				pc.CategoryId
			});

			modelBuilder.Entity<Product_Category>().HasOne(c=>c.ProductsCategory).WithMany(pc => pc.Products_Categories)
				.HasForeignKey(c=>c.CategoryId);

			modelBuilder.Entity<Product_Category>().HasOne(c => c.Product).WithMany(pc => pc.Product_Categories)
				.HasForeignKey(c => c.ProductId);


			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Product> Products { get; set; }

		public DbSet<ProductsCategory> ProductCategories { get; set; }

		public DbSet<Product_Category> Product_Categories { get; set; }

		public DbSet<Sellers> Sellers { get; set; }

		public DbSet<Manufacturer> Manufacturers { get; set; }

		//Orders related tables

		public DbSet<Orders> Orders { get; set; }

		public DbSet<OrderItem> OrderItems { get; set; }

		public DbSet<ShoppingCartItem> ShoppingCartItems { get; set;}
	}
}
