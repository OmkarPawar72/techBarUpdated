using System.ComponentModel.DataAnnotations;
using techBar.Data.Enums;

namespace techBar.Models
{
    public class NewProductsCategoryVM
    {

        public int Id { get; set; }

        [Display(Name ="Product Name")]
        [Required(ErrorMessage ="Name is required")]
        public string? Name { get; set; }

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Display(Name = "Product Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double? Price { get; set; }

        [Display(Name = "Product ImageURL")]
        [Required(ErrorMessage = "Image URL is required")]
        public string? ImageURL { get; set; }

        [Display(Name = "Product StartDate")]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Product EndDate")]
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Product Category")]
        [Required(ErrorMessage = "Product Category is required")]
        public ProductCategory ProductCategory { get; set; }

        //Relationship
        [Display(Name = "Select Product")]
        [Required(ErrorMessage = "Prduct(s) is required")]
        public List<int>? ProductsIds { get; set; }

        //Sellers

        [Display(Name = "Select Seller")]
        [Required(ErrorMessage = "Seller is required")]
        public int? SellerId { get; set; }

        //Manufacturer

        [Display(Name = "Select Manufacturer")]
        [Required(ErrorMessage = "Manufacturer is required")]
        public int? ManufacturerId { get; set; }
    }
}
