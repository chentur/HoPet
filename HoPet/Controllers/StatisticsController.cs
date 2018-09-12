using HoPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectDBContext = HoPet.Models.ProjectDBContext;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HoPet.Controllers
{
    public class StatisticsController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        // GET: Statistics
        public ActionResult Index()
        {
            User sessionUser = (User)HttpContext.Session["user"];
            if (sessionUser != null)
            {
                if (sessionUser.IsAdmin)
                {
                    var petsByType = db.Pets.GroupBy(pet => pet.AnimalType).Select(group => new { type = ((AnimalType)group.Key).ToString(), count = group.Count() }).ToList();
                    var petsByTypeData = JsonConvert.SerializeObject(petsByType);
                    ViewBag.petsByType = petsByTypeData;

                    var petsCountByOrganization = db.Organizations.Include("Pets").Select(org => new { name = org.Name, count = org.Pets.Count() });
                    var petsCountByOrganizationData = JsonConvert.SerializeObject(petsCountByOrganization);
                    ViewBag.petsCountByOrganization = petsCountByOrganizationData;

                    return PartialView();
                }
                else
                {
                    return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
                }
            }

            return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
        }
    }
}