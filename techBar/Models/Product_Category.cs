namespace techBar.Models
{
	public class Product_Category
	{
		public int ProductId { get; set; }

		public Product Product { get; set; }

		public int CategoryId { get; set; }

		public ProductsCategory ProductsCategory { get; set; }


	}
}
