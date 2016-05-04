using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTema2PS.Models;

namespace MVCTema2PS.Controllers
{
    public class ShowController : Controller
    {
        private ShowDBContext db = new ShowDBContext();

        //
        // GET: /Show/

        public ActionResult Index()
        {
            return View(db.Shows.ToList());
        }

        //
        // GET: /Show/Details/5

        public ActionResult Details(int id = 0)
        {
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        //
        // GET: /Show/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Show/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Show show)
        {
            if (ModelState.IsValid)
            {
                db.Shows.Add(show);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(show);
        }

        //
        // GET: /Show/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        //
        // POST: /Show/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Show show)
        {
            if (ModelState.IsValid)
            {
                db.Entry(show).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(show);
        }

        //
        // GET: /Show/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Show show = db.Shows.Find(id);
            if (show == null)
            {
                return HttpNotFound();
            }
            return View(show);
        }

        //
        // POST: /Show/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Show show = db.Shows.Find(id);
            db.Shows.Remove(show);
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