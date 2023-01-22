using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace OnlineCarRental.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Enter Year")]
        public int year { get; set; }

        [Required]
        [Display(Name = "Enter Color")]
        public string color { get; set; }

        [Required]
       
        public string conditon { get; set; }

        [Required]
        [Display(Name = "Enter Rent")]
        public int rent { get; set; }

        [Required]
        [Display(Name = "Enter CarNo")]
        public string carno { get; set; }

        
        [Display(Name ="Image")]
        public string img { get; set; }

      
    }
}