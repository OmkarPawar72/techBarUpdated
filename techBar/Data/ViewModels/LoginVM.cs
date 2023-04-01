using System.ComponentModel.DataAnnotations;

namespace techBar.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name ="Email address")]
        [Required(ErrorMessage ="Email Address is requried")]
        public string EmailAddress { get; set; }

        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
