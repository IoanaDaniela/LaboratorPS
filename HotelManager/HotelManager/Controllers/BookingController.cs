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
    public class BookingController : Controller
    {
        private BookingDBContext db = new BookingDBContext();
        private RoomBookingDBContext dbRoomBooking = new RoomBookingDBContext();
        private RoomDBContext dbRoom = new RoomDBContext();
        private ClientDBContext dbClient = new ClientDBContext();
        //
        // GET: /Booking/



        
        public ActionResult Index()
        {
            return View(db.Bookings.ToList());
        }


        [Authorize(Roles = "Employee")]
        public ActionResult BookingList()
        {
            List<BookingDetails> BookingsList = new List<BookingDetails>();
            List<Booking> AllBookings = db.Bookings.ToList();

            foreach (var booking in AllBookings)
            {
                //Client
                Client client = dbClient.Clients.Find(booking.ClientId);

                //Rooms + TotalPrice
                var roomBookings = from t in dbRoomBooking.RoomBookings select t;
                roomBookings = roomBookings.Where(r => r.BookingId == booking.BookingId); //camerele ce aparti rezervarii curente
                List<Room> rooms = new List<Room>();
                int totalPrice = 0;
                foreach (var roomB in roomBookings)
                {
                    totalPrice += roomB.Price;
                    Room r = dbRoom.Rooms.Find(roomB.RoomId);
                    rooms.Add(r);
                }

                BookingsList.Add(new BookingDetails { client = client, booking = booking, totalPrice = totalPrice, rooms = rooms });
                
            }



            return View(BookingsList);
        }


        [Authorize(Roles = "Employee")]
        public ActionResult DetailsForSpecifiedBooking(int id = 0)
        {
            Booking booking = db.Bookings.Find(id);

            //Client
            Client client = dbClient.Clients.Find(booking.ClientId);

            //Rooms + TotalPrice
            var roomBookings = from t in dbRoomBooking.RoomBookings select t;
            roomBookings = roomBookings.Where(r => r.BookingId == booking.BookingId); //camerele ce aparti rezervarii curente
            List<Room> rooms = new List<Room>();
            int totalPrice = 0;
            foreach (var roomB in roomBookings)
            {
                totalPrice += roomB.Price;
                Room r = dbRoom.Rooms.Find(roomB.RoomId);
                rooms.Add(r);
            }
            BookingDetails bookingDetails = new BookingDetails { client = client, booking = booking, totalPrice = totalPrice, rooms = rooms };

            return View(bookingDetails);
        }

        [HttpPost]
        public ActionResult DetailsForSpecifiedBooking(BookingDetails bookingDetails)
        {

            var roomBookings = from t in dbRoomBooking.RoomBookings select t;
            roomBookings = roomBookings.Where(r => r.BookingId == bookingDetails.booking.BookingId); //camerele ce aparti rezervarii curente
           
            List<Room> rooms = new List<Room>();
            foreach (var roomB in roomBookings)
            {
                Room r = dbRoom.Rooms.Find(roomB.RoomId);
                rooms.Add(r);
            }
            bookingDetails.rooms = rooms;


            InvoiceFactory invoiceFact = InvoiceFactory.getInstance();
            if (Request.Form["PDFInvoiceButton"] != null)
            {
                BookingInvoice e = invoiceFact.exportInvoice(InvoiceFactory.InvoiceTypes.PDF);
                e.export(bookingDetails);

            }
            else if (Request.Form["TxtInvoiceButton"] != null)
            {
                BookingInvoice e = invoiceFact.exportInvoice(InvoiceFactory.InvoiceTypes.TXT);
                Response.Write(e.export(bookingDetails));
                Response.ContentType = "application/txt";
                Response.AppendHeader("Content-Disposition", "attachment; filename=invoice.txt");
                Response.End();
            }
            return RedirectToAction("BookingList");
        }

        //
        // GET: /Booking/Details/5


        public ActionResult Details(int id = 0)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult BookingsReport()
        {
            return View();
        }


        [HttpPost]
        public ActionResult BookingsReport(Booking booking)
        {

            List<BookingDetails> bookingsDetails = new List<BookingDetails>();
            var bookings = from t in db.Bookings select t;
            bookings = bookings.Where(b =>  b.CheckIn.CompareTo(booking.CheckIn) >= 0 &&
                                           b.CheckOut.CompareTo(booking.CheckOut) <= 0
                                    );
            foreach (var book in bookings)
            {
                Client client = dbClient.Clients.Find(book.ClientId);


                var roomBookings = from t in dbRoomBooking.RoomBookings select t;
                roomBookings = roomBookings.Where(r => r.BookingId == book.BookingId); //camerele ce aparti rezervarii curente

                int totalPrice = 0;
                List<Room> rooms = new List<Room>();
                foreach (var roomB in roomBookings)
                {
                    totalPrice += roomB.Price;
                    Room r = dbRoom.Rooms.Find(roomB.RoomId);
                    rooms.Add(r);
                }



                bookingsDetails.Add(new BookingDetails { client = client, booking = book, totalPrice = totalPrice, rooms = rooms });

            }




            ReportFactory reportFact = ReportFactory.getInstance();
            if (Request.Form["CSVReportButton"] != null)
            {
                BookingReport r = reportFact.export(ReportFactory.ReportTypes.CSV);
                Response.Write(r.exportReport(bookingsDetails));
                Response.ContentType = "application/csv";
                Response.AppendHeader("Content-Disposition", "attachment; filename=booking.csv");
                Response.End();
            }
            else if (Request.Form["XMLReportButton"] != null)
            {
                BookingReport r = reportFact.export(ReportFactory.ReportTypes.XML);
                Response.Write(r.exportReport(bookingsDetails));
                Response.ContentType = "application/xml";
                Response.AppendHeader("Content-Disposition", "attachment; filename=booking.xml");
                Response.End();    
            }

            return RedirectToAction("AdminAfterLogin","Account");
        }


        

        //
        // GET: /Booking/Create
        [Authorize(Roles = "Employee")]
        public ActionResult Create(int ClientId)
        {
            Booking bk = new Booking();
            bk.ClientId = ClientId;
            return View(bk);
        }

        //
        // POST: /Booking/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                List<Booking> bookingList = db.Bookings.ToList();

                foreach (var x in bookingList)
                {
                    if ((x.ClientId == booking.ClientId) &&
                        (x.CheckIn.CompareTo(booking.CheckIn) == 0) &&
                        (x.CheckOut.CompareTo(booking.CheckOut) == 0)
                        )
                    {
                        booking = x;
                    }
                }

                return RedirectToAction("ChooseRooms", "Room", new { bookingId = booking.BookingId });   
            }

            return View(booking);
        }


        public ActionResult BookingDetails(int BookingId)
        {
            Booking booking = db.Bookings.Find(BookingId);
            ViewBag.CheckIn = booking.CheckIn.ToString();
            ViewBag.CheckOut = booking.CheckOut.ToString();

            
            var roomBookings = from t in dbRoomBooking.RoomBookings
                               select t;
            roomBookings = roomBookings.Where(r => r.BookingId == BookingId);
            ViewBag.Rooms = "";
            int nrCamera =1;
            double totalPrice = 0;
            foreach(var rb in roomBookings){
                Room room = dbRoom.Rooms.Find(rb.RoomId);
                ViewBag.Rooms += nrCamera.ToString() + ". " + room.Type.ToString() +"( Price: " + rb.Price.ToString() + "$ ) ";
                totalPrice += rb.Price;
                nrCamera++;
            }
            ViewBag.TotalPrice ="Total Price: "+ totalPrice.ToString() + "$";

            Client client = dbClient.Clients.Find(booking.ClientId);
            ViewBag.Client = client.FirstName + " " + client.LastName + " (" + client.Email + ", " + client.MobileNumber + ")";
                
            return View();
        }


        //
        // GET: /Booking/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        //
        // POST: /Booking/Edit/5
        [Authorize(Roles = "Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        //
        // GET: /Booking/Delete/5
        [Authorize(Roles = "Employee")]
        public ActionResult Delete(int id = 0)
        {
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            var roomBookings = from t in dbRoomBooking.RoomBookings select t;
            roomBookings = roomBookings.Where(r => r.BookingId == id);
            foreach (var rB in roomBookings)
            {
                RoomBooking roomB = dbRoomBooking.RoomBookings.Find(rB.ID);
                dbRoomBooking.RoomBookings.Remove(roomB);
            }
            dbRoomBooking.SaveChanges();

            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("BookingList");
        }

        //
        // POST: /Booking/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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