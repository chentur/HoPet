using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoPet.Models
{
    public enum Area
    {
        NORTH, SOUTH, WEST, EAST
    }

    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public Area Area { get; set; }

        // Collections

        public ICollection<Pet> Pets { get; set; }
        public ICollection<AdoptionRequest> AdoptionRequests { get; set; }
    }
}