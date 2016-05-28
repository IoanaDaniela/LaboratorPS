using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManager.Models
{
    public class CheckBoxModel
    {
        public int BookingId { get; set; }
        public int Id { get; set; }
        public string Name {get; set; } 
        public bool Checked {get; set;} 
    }
}