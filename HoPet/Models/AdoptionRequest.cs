using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoPet.Models
{
    public class AdoptionRequest
    {
        public int Id { get; set; }
        public bool IsOpen { get; set; }
        public User User { get; set; }
        public Pet Pet { get; set; }
    }

}