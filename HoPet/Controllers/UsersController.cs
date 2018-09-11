using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HoPet.Models;
using ProjectDBContext = HoPet.Models.ProjectDBContext;

namespace HoPet.Controllers
{
    public class UsersController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        // GET: Users
        public ActionResult Index()
        {
            var currentUser = ((User)HttpContext.Session["user"]);
            if (currentUser != null)
            {
                var users = db.Users.Select(s => s);
                // Only admin is allowed to see al users
                if (!currentUser.IsAdmin)
                {
                    return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
                }
                return View(users.ToList());
            }
            return RedirectToAction("Index", "Error");
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser.Id == id || sessionUser.IsAdmin)
            {
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
            }
            
        }

        // GET: Users/Register
        public ActionResult Register()
        {
            var currentUser = ((User)HttpContext.Session["user"]);
            if (currentUser == null || currentUser.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
            }
        }

        // POST: Users/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,Username,Email,ContactInfo,Password,IsAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Where(curr => curr.Username.Equals(user.Username)).Count() > 0)
                {
                    ViewBag.ErrMsg = "Username already exists, please try again";
                }
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    if (System.Web.HttpContext.Current.Session["user"] == null)
                    {
                        System.Web.HttpContext.Current.Session["user"] = user;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Users");
                    }
                }
            }

            return View(user);
        }

        // GET: Users/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var userFromDB = db.Users.Where(curr =>
                        curr.Username.Equals(username, System.StringComparison.Ordinal) &&
                        curr.Password.Equals(password, System.StringComparison.Ordinal)).SingleOrDefault();
            if (userFromDB != null)
            {
                System.Web.HttpContext.Current.Session["user"] = userFromDB;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrMsg = "Username or password are incorrect.";
            return View();
        }

        // GET: Users/Logoff
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["user"] = null;
            return RedirectToAction("Login", "Users");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser.Id == id || sessionUser.IsAdmin)
            {
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
            }
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Email,ContactInfo,Password,IsAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
                if (sessionUser.IsAdmin)
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    return RedirectToAction("Details", "Users", new { id = sessionUser.Id });
                }
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userFromDB = db.Users.Where(curr => curr.Id == id).SingleOrDefault();
            User user = db.Users.Find(id);

            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { message = "User cannot be deleted - User may have adoption requests" });
            }

            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            // If session user is admin and not the user that is beeing deleted
            if (sessionUser.IsAdmin && sessionUser.Id != id)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                System.Web.HttpContext.Current.Session["user"] = null;
                return RedirectToAction("Login", "Users");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
