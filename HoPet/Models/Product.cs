﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoPet.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public AnimalType PetRelated { get; set; }
        public string Description { get; set; }
    }
}