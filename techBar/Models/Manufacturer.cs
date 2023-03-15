using System.ComponentModel.DataAnnotations;
using techBar.Data.Base;

namespace techBar.Models
{
	public class Manufacturer : IEntityBase
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Profiel Picture")]
		public string ProfilePitctureURL { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        public string Bio { get; set; }

		//Relationship
		
		public List<ProductsCategory> ProductsCategories { get; set; }
	}
}
