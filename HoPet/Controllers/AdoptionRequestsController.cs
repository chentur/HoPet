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
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null)
            {
                ViewBag.IsUserAdmin = sessionUser.IsAdmin;
                ViewBag.UserId = sessionUser.Id;

                return View(db.AdoptionRequests.Include(adopReq => adopReq.Pet).Include(adopReq => adopReq.User).ToList());
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
            }
        }

        // GET: AdoptionRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null)
            {
                AdoptionRequest adoptionRequest = db.AdoptionRequests.Include(adopReq => adopReq.Pet).Include(adopReq => adopReq.User).SingleOrDefault(ar => ar.Id == id);
                if (adoptionRequest == null)
                {
                    return HttpNotFound();
                }

                ViewBag.PetName = adoptionRequest.Pet.Name;
                return View(adoptionRequest);
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
            }
        }

        // GET: AdoptionRequests/Create
        public ActionResult Create()
        {
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null)
            {
                if (sessionUser.Id == db.Users.Find(sessionUser.Id).Id)
                {
                    ViewBag.IsAdmin = sessionUser.IsAdmin;
                    ViewBag.Pets = new SelectList(db.Pets, "Id", "Name");

                    if (sessionUser.IsAdmin)
                    {
                        ViewBag.Users = new SelectList(db.Users, "Id", "Username");
                    }
                    else
                    {
                        ViewBag.Users = db.Users.Find(sessionUser.Id).Username;
                    }

                    return View();
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

        // POST: AdoptionRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IsOpen,Pet_Id,User_Id")] AdoptionRequest adoptionRequest)
        {
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];

            adoptionRequest.Pet = db.Pets.Find(adoptionRequest.Pet_Id);
            adoptionRequest.Pet_Id = db.Pets.Find(adoptionRequest.Pet_Id).Id;
            adoptionRequest.Pet.Organization_Id = adoptionRequest.Pet.Organization.Id;

            if (adoptionRequest.Pet.IsAdopted)
            {
                return RedirectToAction("Index", "Error", new { message = "The pet is alredy adopted" });
            }
            else
            {
                if (adoptionRequest.User_Id == null)
                {
                    adoptionRequest.User = db.Users.Find(sessionUser.Id);
                    adoptionRequest.User_Id = db.Users.Find(sessionUser.Id).Id;
                }
                else
                {
                    adoptionRequest.User = db.Users.Find(adoptionRequest.User_Id);
                    adoptionRequest.User_Id = db.Users.Find(adoptionRequest.User_Id).Id;
                }

                var ar = from b in db.AdoptionRequests.Where(x => x.Pet_Id == adoptionRequest.Pet.Id) select b;

                foreach (AdoptionRequest item in ar)
                {
                    if (item.User_Id == adoptionRequest.User.Id)
                    {
                        return RedirectToAction("Index", "Error", new { message = "The adoption request for " + adoptionRequest.Pet.Name + " alredy exist for " + adoptionRequest.User.Username});
                    }

                }

                if (ModelState.IsValid)
                {
                    adoptionRequest.IsOpen = true;
                    db.AdoptionRequests.Add(adoptionRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(adoptionRequest);
            }
        }

        // GET: AdoptionRequests/CreateWithPet
        public ActionResult CreateWithPet(int? id)
        {
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (db.Pets.Find(id).IsAdopted)
            {
                return RedirectToAction("Index", "Error", new { message = "The pet is alredy adopted" });
            }
            else
            {
                if (sessionUser != null)
                {
                    if (sessionUser.Id == db.Users.Find(sessionUser.Id).Id)
                    {
                        AdoptionRequest adoptionRequest = new AdoptionRequest();
                        adoptionRequest.User = db.Users.Find(sessionUser.Id);
                        adoptionRequest.User_Id = db.Users.Find(sessionUser.Id).Id;
                        adoptionRequest.Pet = db.Pets.Find(id);
                        adoptionRequest.Pet_Id = db.Pets.Find(id).Id;
                        adoptionRequest.Pet.Organization = db.Pets.Find(adoptionRequest.Pet_Id).Organization;
                        adoptionRequest.Pet.Organization_Id = db.Pets.Find(adoptionRequest.Pet_Id).Organization.Id;

                        var ar = from b in db.AdoptionRequests.Where(x => x.Pet_Id == adoptionRequest.Pet.Id) select b;

                        foreach (AdoptionRequest item in ar)
                        {
                            if (item.User_Id == adoptionRequest.User.Id)
                            {
                                return RedirectToAction("Index", "Error", new { message = "The adoption request for " + adoptionRequest.Pet.Name + " alredy exist for " + adoptionRequest.User.Username });
                            }

                        }

                        if (ModelState.IsValid)
                        {
                            adoptionRequest.IsOpen = true;
                            db.AdoptionRequests.Add(adoptionRequest);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }

                        return RedirectToAction("Index", "Error", new { message = "The request didn't save" });
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
        }

        // GET: AdoptionRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null)
            {
                if (sessionUser.Id == db.Users.Find(sessionUser.Id).Id)
                {
                    AdoptionRequest adoptionRequest = db.AdoptionRequests.Include(adopReq => adopReq.Pet).Include(adopReq => adopReq.User).SingleOrDefault(ar => ar.Id == id);

                    if (sessionUser.IsAdmin || sessionUser.Id == adoptionRequest.User.Id)
                    {
                        if (adoptionRequest == null)
                        {
                            return HttpNotFound();
                        }

                        ViewBag.IsAdmin = sessionUser.IsAdmin;
                        ViewBag.Pets = new SelectList(db.Pets, "Id", "Name");

                        if (sessionUser.IsAdmin)
                        {
                            ViewBag.Users = new SelectList(db.Users, "Id", "Username");
                        }
                        else
                        {
                            ViewBag.Users = db.Users.Find(sessionUser.Id).Username;
                        }

                        return View(adoptionRequest);

                    }
                    else
                    {
                        return RedirectToAction("Index", "Error", new { message = "Not autorized!" });
                    }
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

        // POST: AdoptionRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsOpen,Pet_Id,User_Id")] AdoptionRequest adoptionRequest)
        {
            adoptionRequest.Pet = db.Pets.Find(adoptionRequest.Pet_Id);
            adoptionRequest.Pet_Id = db.Pets.Find(adoptionRequest.Pet_Id).Id;
            adoptionRequest.Pet.Organization_Id = db.Pets.Find(adoptionRequest.Pet_Id).Organization.Id;

            if (adoptionRequest.Pet.IsAdopted)
            {
                return RedirectToAction("Index", "Error", new { message = "The pet is alredy adopted" });
            }
            else
            {
                User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
                adoptionRequest.User = db.Users.Find(adoptionRequest.User_Id);
                adoptionRequest.User_Id = db.Users.Find(adoptionRequest.User_Id).Id;

                var ar = from b in db.AdoptionRequests.Where(x => x.Pet_Id == adoptionRequest.Pet.Id) select b;

                foreach (AdoptionRequest item in ar)
                {
                    if (item.User_Id == adoptionRequest.User.Id)
                    {
                        return RedirectToAction("Index", "Error", new { message = "The adoption request for " + adoptionRequest.Pet.Name + " alredy exist for " + adoptionRequest.User.Username });
                    }

                }

                if (ModelState.IsValid)
                {
                    adoptionRequest.IsOpen = true;
                    db.Entry(adoptionRequest).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(adoptionRequest);
            }

            if (ModelState.IsValid)
            {
                db.Entry(adoptionRequest).State = EntityState.Modified;
                db.Entry(adoptionRequest.Pet).State = EntityState.Modified;
                db.Entry(adoptionRequest.User).State = EntityState.Modified;

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
            User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];
            if (sessionUser != null)
            {
                AdoptionRequest adoptionRequest = db.AdoptionRequests.Include(adopReq => adopReq.Pet).Include(adopReq => adopReq.User).SingleOrDefault(ar => ar.Id == id);

                if (adoptionRequest == null)
                {
                    return HttpNotFound();
                }
                else if (sessionUser.IsAdmin || sessionUser.Id == adoptionRequest.User.Id)
                {
                    string UserName = db.Users.Find(adoptionRequest.User.Id).Username;
                    ViewBag.UserName = UserName;
                    string PetName = db.Pets.Find(adoptionRequest.Pet.Id).Name;
                    ViewBag.PetName = PetName;

                    return View(adoptionRequest);
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

        // POST: AdoptionRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdoptionRequest adoptionRequest = db.AdoptionRequests.Find(id);
            db.AdoptionRequests.Remove(adoptionRequest);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: AdoptionRequests/Aprove/6
        public ActionResult Aprove(int id)
        {
            AdoptionRequest adoptionRequest = db.AdoptionRequests.Find(id);

            if (!adoptionRequest.Pet.IsAdopted)
            {
                User sessionUser = (User)System.Web.HttpContext.Current.Session["user"];

                if (sessionUser.IsAdmin || sessionUser.Id == adoptionRequest.User.Id)
                {
                    adoptionRequest.IsOpen = false;
                    adoptionRequest.Pet.IsAdopted = true;

                    var ar = from b in db.AdoptionRequests select b;

                    ar = ar.Where(x => x.Pet_Id == adoptionRequest.Pet.Id);

                    foreach (AdoptionRequest item in ar)
                    {
                        if(item.User_Id != adoptionRequest.User.Id)
                        {
                            db.AdoptionRequests.Remove(item);
                        }
                        
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Error", new { message = "You have to login first.." });
                }
            }
            else
            {
                return RedirectToAction("Index", "Error", new { message = "The pet is alredy adopte" });
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
