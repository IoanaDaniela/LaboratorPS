using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelManager.Models
{
    public class Booking
    {
        
        [Required]
        public int BookingId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
    }


    public class BookingDBContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Client> Clients { get; set; }
    }

}