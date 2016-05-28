using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelManager.Models
{
    public class Room
    {
        [Required]
        public int RoomId { get; set; }

        [Required]
        public int RoomNumber { get; set; }
        
        [Required]
        public string Type { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public int Rate { get; set; }
    }


    public class RoomDBContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
    }

}