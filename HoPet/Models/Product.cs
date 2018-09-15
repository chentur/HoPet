using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoPet.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter product name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter price")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Please enter quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Product created for")]
        public AnimalType PetRelated { get; set; }
        public string Description { get; set; }
    }
}