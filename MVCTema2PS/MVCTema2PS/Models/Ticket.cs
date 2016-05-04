using System;
using System.Data.Entity;

namespace MVCTema2PS.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public int Row { get; set; }
        public int SeatNumber { get; set; }
        public int ShowId { get; set; }
    }


    public class TicketDBContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
    }

}