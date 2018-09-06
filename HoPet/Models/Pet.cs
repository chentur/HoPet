using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public DateTime Birthdate { get; set; }
        public DateTime AdoptionDate { get; set; }
        public bool IsAdopted { get; set; }
        public AnimalType AnimalType { get; set; }
        public string Description { get; set; }
    }
}