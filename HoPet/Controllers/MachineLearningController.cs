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
using Microsoft.ML.Runtime;
using System.Net;

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
            pipeline.Add(new StochasticDualCoordinateAscentClassifier
            {
                MaxIterations = 100,
                NumThreads = 7,
                LossFunction = new SmoothedHingeLossSDCAClassificationLossFunction()
            });
            pipeline.Add(new PredictedLabelColumnOriginalValueConverter() { PredictedLabelColumn = "PredictedLabel" });

            this.model = pipeline.Train<PetNameData, PetNamePrediction>();
        }

        // GET: MachineLearning
        public ActionResult Index()
        {
            return View();
        }

        // GET: MachineLearning/GetPetNamePrediction
        public Object GetPetNamePrediction(string year)
        {
            try
            {
                int intYear = Int32.Parse(year);
                var prediction = this.model.Predict(new PetNameData { Year = intYear });
                return prediction.PredictedName;
            }
            catch (FormatException e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}