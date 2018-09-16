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
    public class PetsController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        // GET: Pets
        public ActionResult Index()
        {
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null)
            {
                return View(db.Pets.Include(pet => pet.Organization).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
            }
        }

        // GET: Pets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null)
            {
                Pet pet = db.Pets.Include(p => p.Organization).SingleOrDefault(pt => pt.Id == id);
                if (pet == null)
                {
                    return HttpNotFound();
                }
                return View(pet);
            } else
            {
                return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
            }
        }

        // GET: Pets/Create
        public ActionResult Create()
        {
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null)
            {
                if (sessionUser.IsAdmin)
                {
                    ViewBag.Organizations = new SelectList(db.Organizations, "Id", "Name");
                    return View();
                } else
                {
                    return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
                }
            } else
            {
                return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
            }

        }

        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Age,IsAdopted,AnimalType,Description,Organization_Id")] Pet pet)
        {
            Organization organization = db.Organizations.Find(pet.Organization_Id);
            pet.Organization = organization;
            if (ModelState.IsValid)
            {
                db.Pets.Add(pet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pet);
        }

        // GET: Pets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null)
            {
                if (sessionUser.IsAdmin)
                {
                    Pet pet = db.Pets.Include(p => p.Organization).SingleOrDefault(pt => pt.Id == id);
                    if (pet == null)
                    {
                        return HttpNotFound();
                    }

                    ViewBag.Organizations = new SelectList(db.Organizations, "Id", "Name");
                    return View(pet);
                } else
                {
                    return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
                }
            } else
            {
                return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
            }
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,IsAdopted,AnimalType,Description,Organization_Id")] Pet pet)
        {
            Organization organization = db.Organizations.Find(pet.Organization_Id);
            pet.Organization = organization;
            if (ModelState.IsValid)
            {
                db.Entry(pet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pet);
        }

        // GET: Pets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null)
            {
                if (sessionUser.IsAdmin)
                {
                    Pet pet = db.Pets.Include(p => p.Organization).SingleOrDefault(pt => pt.Id == id);
                    if (pet == null)
                    {
                        return HttpNotFound();
                    }
                    string OrganizationName = db.Organizations.Find(pet.Organization.Id).Name;
                    ViewBag.OrganizationName = OrganizationName;
                    return View(pet);
                }
                else
                {
                    return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
                }
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
            }
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pet pet = db.Pets.Find(id);
            db.Pets.Remove(pet);
            db.SaveChanges();
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
