using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HoPet.Models
{
    public class User
    {
        private const int SIZE = 50;

        public int Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(SIZE)]
        [Required(ErrorMessage = "Please enter a username")]
        [Index(IsUnique = true)]
        public string Username { get; set; }

        public string Email { get; set; }

        [Display(Name = "Contact info")]
        public string ContactInfo { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }

        // Collections
        public ICollection<AdoptionRequest> AdoptionRequests { get; set; }
    }
}