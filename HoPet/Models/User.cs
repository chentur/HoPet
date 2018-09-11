using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoPet.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a username")]
        public string Username { get; set; }

        public string Email { get; set; }

        [Display(Name = "Contact info")]
        public string ContactInfo { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }
    }
}