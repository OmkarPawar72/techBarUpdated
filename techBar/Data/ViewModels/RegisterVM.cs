﻿using System.ComponentModel.DataAnnotations;

namespace techBar.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is requried")]
        public string FullName { get; set; }

        [Display(Name ="Email address")]
        [Required(ErrorMessage ="Email Address is requried")]
        public string EmailAddress { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="ConfrimPassword")]
        [Required(ErrorMessage ="Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
