using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HoPet.Models
{
    public class AdoptionRequest
    {
        public int Id { get; set; }

        [Display(Name = "Status")]
        public bool IsOpen { get; set; }

        [ForeignKey("User")]
        public int? User_Id { get; set; }

        public virtual User User { get; set; }

        [ForeignKey("Pet")]
        public int? Pet_Id { get; set; }

        public virtual Pet Pet { get; set; }
    }

}