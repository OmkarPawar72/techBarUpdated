using System.ComponentModel.DataAnnotations;
using techBar.Data.Base;

namespace techBar.Models
{
	public class Sellers : IEntityBase
    {
		[Key]
		public int Id { get; set; }

		[Display(Name = "Profile Logo")]
		public string logo { get; set; }

        [Display(Name = "Name")]
        public string SellerName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

		//Relationships

		public List<ProductsCategory> ProductsCategories { get; set; }
	}
}
