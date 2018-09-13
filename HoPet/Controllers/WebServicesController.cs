using HoPet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoPet.Controllers
{
    public class WebServicesController : Controller
    {
        // GET: WebServices/Wiki
        public ActionResult Wiki()
        {
            return PartialView();
        }

        // GET : WebServices/GetWikiValue/title
        public String GetWikiValue(string title)
        {
            WebServices webServices = new WebServices();
            return webServices.GetWikiValue(title);
        }
    }
}