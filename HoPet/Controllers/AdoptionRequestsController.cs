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
    public class AdoptionRequestsController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        // GET: AdoptionRequests
        public ActionResult Index()
        {
            return View(db.adoptionRequests.Include(p => p.Pet).Include(u => u.User).ToList());
        }

        // GET: AdoptionRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdoptionRequest adoptionRequest = null;
            foreach(AdoptionRequest ar in db.adoptionRequests.Include(p => p.Pet).Include(o => o.Pet.Organization).Include(u => u.User))
            {
                if (ar.Id == id)
                    adoptionRequest = ar;
            }
            if (adoptionRequest == null)
            {
                return HttpNotFound();
            }
            return View(adoptionRequest);
        }

        // GET: AdoptionRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdoptionRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsOpen")] AdoptionRequest adoptionRequest)
        {
            if (ModelState.IsValid)
            {
                db.adoptionRequests.Add(adoptionRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adoptionRequest);
        }

        // GET: AdoptionRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdoptionRequest adoptionRequest = db.adoptionRequests.Find(id);
            if (adoptionRequest == null)
            {
                return HttpNotFound();
            }
            return View(adoptionRequest);
        }

        // POST: AdoptionRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsOpen")] AdoptionRequest adoptionRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adoptionRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adoptionRequest);
        }

        // GET: AdoptionRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdoptionRequest adoptionRequest = db.adoptionRequests.Find(id);
            if (adoptionRequest == null)
            {
                return HttpNotFound();
            }
            return View(adoptionRequest);
        }

        // POST: AdoptionRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdoptionRequest adoptionRequest = db.adoptionRequests.Find(id);
            db.adoptionRequests.Remove(adoptionRequest);
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
