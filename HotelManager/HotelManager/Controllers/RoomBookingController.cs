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
    public class RoomBookingController : Controller
    {
        private RoomBookingDBContext db = new RoomBookingDBContext();
        private BookingDBContext dbBooking = new BookingDBContext();
        private RoomDBContext dbRoom = new RoomDBContext();
        //
        // GET: /RoomBooking/

        public ActionResult Index()
        {
            return View(db.RoomBookings.ToList());
        }

        //
        // GET: /RoomBooking/Details/5

        public ActionResult Details(int id = 0)
        {
            RoomBooking roombooking = db.RoomBookings.Find(id);
            if (roombooking == null)
            {
                return HttpNotFound();
            }
            return View(roombooking);
        }

        //
        // GET: /RoomBooking/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /RoomBooking/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomBooking booking)
        {
            if (ModelState.IsValid)
            {
                db.RoomBookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        //
        // GET: /RoomBooking/Edit/5


        public ActionResult SaveBookings()
        {

            List<CheckBoxModel> Rooms = TempData["list"] as List<CheckBoxModel>;
            foreach (var room in Rooms)
            {
                Booking booking = dbBooking.Bookings.Find(room.BookingId);
                int noDays = (int)(booking.CheckOut - booking.CheckIn).TotalDays;
                Room r = dbRoom.Rooms.Find(room.Id);
                int price = r.Rate * noDays;
                RoomBooking rb = new RoomBooking { BookingId = room.BookingId, RoomId = room.Id, Price = price };
                db.RoomBookings.Add(rb);
                

            }
            db.SaveChanges();   

            return RedirectToAction("BookingDetails", "Booking", new { BookingId = Rooms.First().BookingId });
        }


        public ActionResult Edit(int id = 0)
        {
            RoomBooking roombooking = db.RoomBookings.Find(id);
            if (roombooking == null)
            {
                return HttpNotFound();
            }
            return View(roombooking);
        }

        //
        // POST: /RoomBooking/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoomBooking roombooking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roombooking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roombooking);
        }

        //
        // GET: /RoomBooking/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RoomBooking roombooking = db.RoomBookings.Find(id);
            if (roombooking == null)
            {
                return HttpNotFound();
            }
            return View(roombooking);
        }

        //
        // POST: /RoomBooking/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomBooking roombooking = db.RoomBookings.Find(id);
            db.RoomBookings.Remove(roombooking);
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