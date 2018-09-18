using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HoPet.Models
{
    public enum AnimalType
    {
        DOG, CAT, HAMSTER, PARROT, RAT, FISH, FROG, GUINEAPIG, RABBIT, SNAKE
    }

    public class Pet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter pet name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter pet age")]
        [Range(0.0, 200)]
        public double Age { get; set; }

        [Display(Name = "Adopted")]
        [DefaultValue(false)]
        public bool IsAdopted { get; set; }

        [Display(Name = "Animal Kind")]
        [Required(ErrorMessage = "Please choose pet kind")]
        public AnimalType AnimalType { get; set; }

        public string Description { get; set; }

        [ForeignKey("Organization")]
        public int? Organization_Id { get; set; }
        public virtual Organization Organization { get; set; }

        // Collections

        public ICollection<AdoptionRequest> AdoptionRequests { get; set; }
    }
}