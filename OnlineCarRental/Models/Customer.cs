using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineCarRental.Models
{
    public class Customer
    {
        public int id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }


        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Please enter Phone Number")]
        [MinLength(11), MaxLength(11)]
        public string phone { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Please enter email")]
        [Display(Name = "Email")]
        public string email { get; set; }


        [Required(ErrorMessage = "Please Enter confirm Password")]
        [Display(Name = "Confirm Password")]
        [MinLength(8), MaxLength(16)]
        public string password { get; set; }

        [Required(ErrorMessage = "Please Enter confirm Password")]
        [Display(Name = "Confirm Password")]
        [Compare("password")]
        public string confirmPassword { get; set; }
    }
}