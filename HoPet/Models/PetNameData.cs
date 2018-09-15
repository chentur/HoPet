using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoPet.Models
{
    public class PetNameData
    {
        [Column(ordinal: "0", name: "Label")]
        public string PetName;

        [Column(ordinal: "1")]
        public float Year;
    }
}