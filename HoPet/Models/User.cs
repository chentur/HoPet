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
        public string Username { get; set; }
        public string Email { get; set; }
        public string ContactInfo { get; set; }
        public string Password { get; set; }

        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }
    }
}