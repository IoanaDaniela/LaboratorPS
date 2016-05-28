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
    public class RoomController : Controller
    {
        private RoomDBContext db = new RoomDBContext();
        private RoomBookingDBContext dbRoomBooking = new RoomBookingDBContext();
        private BookingDBContext dbBooking = new BookingDBContext();
        //
        // GET: /Room/

        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            return View(db.Rooms.ToList());
        }

        [Authorize(Roles = "Employee")]
        public ActionResult ChooseRooms(int bookingId)
        {
            List<Room> rooms = db.Rooms.ToList();
            var AvailableRooms = new List<CheckBoxModel>();
            Booking booking = dbBooking.Bookings.Find(bookingId);

            foreach (var room in rooms)
            {
                var roomBookings = from t in dbRoomBooking.RoomBookings
                                   select t;
                roomBookings = roomBookings.Where(rb => rb.RoomId == room.RoomId); // in roomBookings extragen rezervarile facute pentru camera curenta

                bool ok = true;
                foreach (var roombooking in roomBookings)//pentru fiecare rezervare verificam daca intervalul se suprapune cu cel curet
                {
                    var bookings = from t in dbBooking.Bookings
                                   select t;
                    bookings = bookings.Where(b => b.BookingId == roombooking.BookingId); //extragem datele pentru rezervarile camerei respective
                    bookings = bookings.Where(b => (b.CheckIn.CompareTo(booking.CheckIn) >= 0 && b.CheckIn.CompareTo(booking.CheckOut) <= 0) ||
                                        (b.CheckOut.CompareTo(booking.CheckIn) >0 && b.CheckOut.CompareTo(booking.CheckOut)<0) ||
                                        (b.CheckIn.CompareTo(booking.CheckIn) <=0 && b.CheckOut.CompareTo(booking.CheckOut)>=0)
                                   ); //verificam daca sunt rezervari ce se suprapun
                    if (bookings.Count() > 0) //exista rezervari ce se suprapun
                    {
                        ok = false;
                    }
                }
                if (ok)// nu avem rezervari ce se suprapun
                {
                 
                    string name = room.RoomNumber.ToString() +"  "+ room.Type +"  "+room.Rate.ToString() + " $";
                    CheckBoxModel checkBox = new CheckBoxModel { BookingId = booking.BookingId, Id = room.RoomId, Name = name, Checked = false };
                    AvailableRooms.Add(checkBox);
                }
                
            }
            return View(AvailableRooms);
        }

        [HttpPost]
        public ActionResult ChooseRooms(List<CheckBoxModel> Rooms)
        {
            List<CheckBoxModel> chekedRooms = new List<CheckBoxModel>();
            foreach (var room in Rooms)
            {
                if (room.Checked)
                {
                    chekedRooms.Add(room);
                }
            }

            TempData["list"] = chekedRooms;
            return RedirectToAction("SaveBookings", "RoomBooking");
        }





        //
        // GET: /Room/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id = 0)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        //
        // GET: /Room/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Room/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room);
        }

        //
        // GET: /Room/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        //
        // POST: /Room/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Room/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        //
        // POST: /Room/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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