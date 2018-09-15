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
    public class ProductsController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        // GET: Products
        public ActionResult Index()
        {
            var sessionUser = ((User)HttpContext.Session["user"]);
            if (sessionUser != null)
            {
                ViewBag.IsUserAdmin = sessionUser.IsAdmin;
                return View(db.Products.ToList());
            }
            return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
        }

        // GET: Products/Create
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

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Quantity,PetRelated,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Price < 0 || product.Price == 0)
                {
                    ViewBag.ErrMsg = "The price need to be bigger then ziro";
                }
                else if (product.Quantity < 0)
                {
                    ViewBag.ErrMsg = "The quantity need to be positive number";
                }
                else if (db.Products.Where(curr => (curr.Name == product.Name && curr.PetRelated == product.PetRelated)).Count() > 0)
                {
                    ViewBag.ErrMsg = "This product already exists to this pet";
                }
                else
                {
                    product.Name = product.Name.ToUpper();
                    if(product.Description != null)
                        product.Description = product.Description.ToLower();
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null && sessionUser.IsAdmin)
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
            }
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Quantity,PetRelated,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Price < 0 || product.Price == 0)
                {
                    ViewBag.ErrMsg = "The price need to be bigger then ziro";
                }
                else if (product.Quantity < 0)
                {
                    ViewBag.ErrMsg = "The quantity need to be positive number";
                }
                else
                {
                    product.Name = product.Name.ToUpper();
                    if (product.Description != null)
                        product.Description = product.Description.ToLower();
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null && sessionUser.IsAdmin)
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            try
            {
                db.Products.Remove(product);
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
