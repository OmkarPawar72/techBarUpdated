using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using techBar.Data.Base;
using techBar.Data.Enums;

namespace techBar.Models
{
    public class ProductsCategory : IEntityBase
    {
		[Key]
		public int Id { get; set; }

		public string? Name { get; set; }	

		public string? Description { get; set; }

		public double Price { get; set; }

		public string? ImageURL { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public ProductCategory ProductCategory { get; set; }

		//Relationship
		public List<Product_Category>? Products_Categories { get; set; }
		 
		//Sellers

		public int SellerId { get; set; }

		[ForeignKey("SellerId")]
		public Sellers? Sellers { get; set; }

		//Manufacturer

		public int ManufacturerId { get; set; }

		[ForeignKey("ManufacturerId")]
		public Manufacturer? Manufacturer { get; set; }
	}
}
