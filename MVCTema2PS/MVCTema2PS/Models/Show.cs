using System;
using System.Data.Entity;

namespace MVCTema2PS.Models
{
    public class Show
    {
        public int ShowID { get; set; }
        public string ShowName { get; set; }
        public string Director { get; set; }
        public string Distribution { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Tickets { get; set; }
        public int AvailableTickets { get; set; }

    }


    public class ShowDBContext : DbContext
    {
        public DbSet<Show> Shows { get; set; }
    }
}