using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTema2PS.Models;
using System.Xml.Linq;

namespace MVCTema2PS.Controllers
{
    public class TicketController : Controller
    {
        private TicketDBContext db = new TicketDBContext();
        private ShowDBContext showDB = new ShowDBContext();
        //
        // GET: /Ticket/

        public ActionResult Index()
        {
            return View(db.Tickets.ToList());
        }

        //
        // GET: /Ticket/Details/5

        public ActionResult Details(int id = 0)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        //
        // GET: /Ticket/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Ticket/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticket);
        }



        public ActionResult AddTicketForSpecifiedShow(int showId = 0)
        {
            Show show = showDB.Shows.Find(showId);
            
            if (show == null)
            {
                return HttpNotFound();
            }

            if (show.AvailableTickets == 0)
            {
                ViewBag.AvailableTickets = "No Tickets Available";
            }
            else
            {
                ViewBag.AvailableTickets = "Number of Available Tickets: " + show.AvailableTickets;   
            }

            Ticket ticket = new Ticket();
            ticket.ShowId = show.ShowID;
            return View(ticket);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTicketForSpecifiedShow(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                Show show = showDB.Shows.Find(ticket.ShowId);

                var tickets = from t in db.Tickets
                              select t;
                if( tickets.Count(s => s.Row == ticket.Row && s.SeatNumber == ticket.SeatNumber) == 0){
                    db.Tickets.Add(ticket);
                    db.SaveChanges();

                    //actualizare bilete disponibile
                    show.AvailableTickets = show.AvailableTickets - 1;
                    showDB.Entry(show).State = EntityState.Modified;
                    showDB.SaveChanges();

                    return RedirectToAction("EmployeeAfterLogin","User");
                }
                else
                {
                    ViewBag.AvailableTickets = "Number of Available Tickets: " + show.AvailableTickets;
                    ViewBag.BiletExistent = "Loc Ocupat!";
                    return View(ticket);
                }

                
            }

            return View(ticket);
        }


        public ActionResult TicketsListForSpecifiedShow(int showId = 0)
        {
            Show show = showDB.Shows.Find(showId);
            
            if (show == null)
            {
                return HttpNotFound();
            }

            var tickets = from t in db.Tickets
                          select t;

            tickets = tickets.Where(s => s.ShowId == showId);
            
            if (tickets.Count() == 0)
            {
                ViewBag.NoTickets = "No tickets for this show!";
            }

            return View(tickets);
        }



        public ActionResult XMLExportForSpecifiedShow(int showId = 0)
        {
            Show show = showDB.Shows.Find(showId);
            
            if (show == null)
            {
                return HttpNotFound();
            }


            List<Ticket> tickets = db.Tickets.ToList();
            tickets.Where(s => s.ShowId == showId); 


            if (tickets.Count() > 0)
            {
                var xEle = new XElement("Tickets",
                        from tck in tickets
                        select new XElement("Ticket",
                           new XElement("TicketID", tck.TicketID),
                           new XElement("Row", tck.Row),
                           new XElement("SeatNumber", tck.SeatNumber),
                           new XElement("ShowID", tck.ShowId)
                            ));
                
                Response.Write(xEle);
                Response.ContentType = "application/xml";
                Response.AppendHeader("Content-Disposition", "attachment; filename=tickets.xml");
                Response.End();
            }

            return RedirectToAction("EmployeeAfterLogin","User"); ;
        }

        //
        // GET: /Ticket/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        //
        // POST: /Ticket/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        //
        // GET: /Ticket/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        //
        // POST: /Ticket/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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