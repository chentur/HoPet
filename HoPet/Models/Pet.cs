using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HoPet.Models
{
    public enum AnimalType
    {
        DOG, CAT, HAMSTER, PARROT, RAT, FISH
    }

    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{dd MM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }
        public DateTime AdoptionDate { get; set; }

        [Display(Name = "Adopted")]
        public bool IsAdopted { get; set; }

        [Display(Name = "Animal Kind")]
        public AnimalType AnimalType { get; set; }
        public string Description { get; set; }
        public Organization Organization { get; set; }
    }
}