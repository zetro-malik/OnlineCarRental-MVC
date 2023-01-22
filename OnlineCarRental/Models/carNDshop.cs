using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineCarRental.Models
{
    public class carNDshop
    {
        public int Id { get; set; }

       [Display(Name="Model")]
        public int year { get; set; }

        [Display(Name = "Color")]
        public string color { get; set; }

        [Display(Name = "Condition")]
        public string conditon { get; set; }

        [Display(Name = "Rent")]
        public int rent { get; set; }

        [Display(Name = "Can no:")]
        public string carno { get; set; }

        public string img { get; set; }

        public int shopid   { get; set; }

        [Display(Name = "Shop Name")]
        public string shopname { get; set; }

        [Display(Name = "Address")]
        public string address { get; set; }

        [Display(Name = "Phone")]
        public string phone { get; set; }

        public string email { get; set; }


    }
}