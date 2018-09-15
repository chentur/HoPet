using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HoPet.Models;

namespace HoPet.Controllers
{
    public class OrganizationsController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        // GET: Organizations
        public ActionResult Index()
        {
            var sessionUser = ((User)HttpContext.Session["user"]);
            if (sessionUser != null)
            {
                ViewBag.IsUserAdmin = sessionUser.IsAdmin;
                return View(db.Organizations.ToList());
            }
            return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
        }

        // GET: Organizations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = db.Organizations.Find(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            var sessionUser = ((User)HttpContext.Session["user"]);
            if (sessionUser != null)
            {
                ViewBag.IsUserAdmin = sessionUser.IsAdmin;
                return View(organization);
            }
            return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
        }

        // GET: Organizations/Create
        public ActionResult Create()
        {
            var sessionUser = ((User)HttpContext.Session["user"]);
            if (sessionUser != null && sessionUser.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
            }
        }

        // POST: Organizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,PhoneNumber,Area")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                if (organization.PhoneNumber.Length != 10)
                {
                    ViewBag.ErrMsg = "The phone number need to be 10 digits";
                }
                else if (db.Organizations.Where(curr => curr.Name == organization.Name).Count() > 0)
                {
                    ViewBag.ErrMsg = "This organization already exists";
                }
                else
                {
                    if (organization.Description != null)
                        organization.Description = organization.Description.ToLower();
                    db.Organizations.Add(organization);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(organization);
        }

        // GET: Organizations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null && sessionUser.IsAdmin)
            {
                Organization organization = db.Organizations.Find(id);
                if (organization == null)
                {
                    return HttpNotFound();
                }
                return View(organization);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
            }
        }

        // POST: Organizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,PhoneNumber,Area")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                if (organization.PhoneNumber.Length != 10)
                {
                    ViewBag.ErrMsg = "The phone number need to be 10 digits";
                }
                else
                {
                    if (organization.Description != null)
                        organization.Description = organization.Description.ToLower();
                    db.Entry(organization).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(organization);
        }

        // GET: Organizations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null && sessionUser.IsAdmin)
            {
                Organization organization = db.Organizations.Find(id);
                if (organization == null)
                {
                    return HttpNotFound();
                }
                return View(organization);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
            }
        }

        // POST: Organizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organization organization = db.Organizations.Find(id);
            try
            {
                db.Organizations.Remove(organization);
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("Index", "Error", new { message = "User cannot be deleted - User may have adoption requests" });
            }
            return RedirectToAction("Index");
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
