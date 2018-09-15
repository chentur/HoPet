using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoPet.Models
{
    public class PetNamePrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedName;
    }
}