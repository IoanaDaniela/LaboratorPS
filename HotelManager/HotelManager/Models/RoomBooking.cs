using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelManager.Models
{
    public class RoomBooking
    {
        public int ID { get; set;}
        [Required]
        public int RoomId { get; set; }

        [Required]
        public int BookingId { get; set; }


        public int Price { get; set; }
    }


    public class RoomBookingDBContext : DbContext
    {
        public DbSet<RoomBooking> RoomBookings { get; set; }
    }

}