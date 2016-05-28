using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelManager.Models
{
    public class BookingDetails
    {
       
        public Client client { set; get; }
        public Booking booking { set; get; }

        [Display(Name = "Total Price ")]
        [DataType(DataType.Currency)]
        public int totalPrice { set; get; }
        public List<Room> rooms { set; get; } 
    }
}