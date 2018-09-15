using HoPet.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Hosting;

namespace HoPet.Controllers
{
    public class MachineLearningController : Controller
    {
        private PredictionModel<PetNameData, PetNamePrediction> model;

        public MachineLearningController()
        {
            string path = HostingEnvironment.MapPath(@"~/Content/ML/PetNamesByYear.csv");

            var pipeline = new LearningPipeline();

            pipeline.Add(new TextLoader(path).CreateFrom<PetNameData>(useHeader: false, separator: ','));
            pipeline.Add(new Dictionarizer("Label"));
            pipeline.Add(new ColumnConcatenator("Features", "Year"));
            pipeline.Add(new StochasticDualCoordinateAscentClassifier());
            pipeline.Add(new PredictedLabelColumnOriginalValueConverter() { PredictedLabelColumn = "PredictedLabel" });

            this.model = pipeline.Train<PetNameData, PetNamePrediction>();
        }

        // GET: MachineLearning
        public ActionResult Index()
        {
            return View();
        }

        // GET: MachineLearning/GetPetNamePrediction
        public string GetPetNamePrediction(int age)
        {
            var birthYear = 2017 - age;
            var prediction = this.model.Predict(new PetNameData { Year = birthYear });
            return prediction.PredictedName;
        }
    }
}