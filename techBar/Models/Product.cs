
using System.ComponentModel.DataAnnotations;
using techBar.Data.Base;

namespace techBar.Models
{
	public class Product : IEntityBase
	{
		[Key]
		public int Id { get; set; }

		[Display(Name ="Profile Picture")]
		public string ProfilePitctureURL { get; set; }


        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Bio Graphy")]
        public string Bio { get; set; }

		//Relationship


		public List<Product_Category> Product_Categories { get; set; }
	}
}
