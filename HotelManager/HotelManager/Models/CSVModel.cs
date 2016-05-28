using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManager.Models
{
    public class CSVModel
    {
       
        public int BookingId { get; set; }


        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int ClientId { get; set; }

        public string Email { get; set; }
  
        public string FirstName { get; set; }

        public string LastName { get; set; }
   
        public string MobileNumber { get; set; }

        public int RoomId { get; set; }

        public int RoomNumber { get; set; }

        public string Type { get; set; }

        public int Rate { get; set; }
        public int TotalPrice { get; set; }
    }
}