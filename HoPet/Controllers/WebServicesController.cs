using HoPet.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoPet.Controllers
{
    public class WebServicesController : Controller
    {
        WebServices webServices = new WebServices();

        // GET: WebServices/Wiki
        public ActionResult Wiki()
        {
            var sessionUser = ((User)HttpContext.Session["user"]);
            if (sessionUser != null)
            {
                if (!sessionUser.IsAdmin)
                {
                    return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
                }
                return PartialView();
            }
            return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
        }

        // GET : WebServices/GetWikiValue/title
        public String GetWikiValue(string title)
        {
            var sessionUser = ((User)HttpContext.Session["user"]);
            if (sessionUser != null && sessionUser.IsAdmin)
            {
                return webServices.GetWikiValue(title);
            }
            return null;
        }

        // GET: WebServices/Twitter
        public ActionResult Twitter()
        {
            var sessionUser = ((User)HttpContext.Session["user"]);
            if (sessionUser != null)
            {
                if (!sessionUser.IsAdmin)
                {
                    return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
                }
                return View();
            }
            return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
        }

        // GET: WebServices/GetTwitterPosts
        public string GetTwitterPosts()
        {
            var sessionUser = ((User)HttpContext.Session["user"]);
            if (sessionUser != null && sessionUser.IsAdmin)
            {
                return JsonConvert.SerializeObject(webServices.GetTweets());
            }
            return null;
        }

        // GET: WebServices/PostTweet
        public void PostTweet(string content)
        {
            var sessionUser = ((User)HttpContext.Session["user"]);
            if (sessionUser != null && sessionUser.IsAdmin)
            {
                webServices.PostTweet(content);
            }
        }
    }
}