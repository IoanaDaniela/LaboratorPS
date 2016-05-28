using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HotelManager.Models;

namespace HotelManager.Controllers
{
    public class ClientController : Controller
    {
        private ClientDBContext db = new ClientDBContext();

        //
        // GET: /Client/
        [Authorize(Roles = "Employee")]
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        //
        // GET: /Client/Details/5
        [Authorize(Roles = "Employee")]
        public ActionResult Details(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //
        // GET: /Client/Create

        [Authorize(Roles = "Employee")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Client/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                
                var c = from t in db.Clients
                        select t;
                c = c.Where(a => a.Email.Equals(client.Email));
                if (c.Count()==0) //Adaugam clientul
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                    
                }
                c = from t in db.Clients
                    select t;
                c = c.Where(x => x.Email.Equals(client.Email));
                int ClientId = c.First().ClientId;
                return RedirectToAction("Create", "Booking", new { ClientId = ClientId });
            }
            return View(client);
        }

        //
        // GET: /Client/Edit/5
        [Authorize(Roles = "Employee")]
        public ActionResult Edit(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //
        // POST: /Client/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        //
        // GET: /Client/Delete/5
        [Authorize(Roles = "Employee")]
        public ActionResult Delete(int id = 0)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //
        // POST: /Client/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}