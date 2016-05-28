using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManager.Models
{
    public class TXTInvoice : BookingInvoice
    {
        public string export(BookingDetails bookingDetails)
        {

            string invoice = "HotelManagent\n";


            invoice += "Client Details:\n";
            invoice += "FirstName:" + bookingDetails.client.FirstName + "\n";
            invoice += "LastName:" + bookingDetails.client.LastName   + "\n";
            invoice += "Email:" + bookingDetails.client.Email + "\n";
            invoice += "Mobile No.:" + bookingDetails.client.MobileNumber + "\n\n";

            invoice += "Booking Details:\n";
            invoice += "CheckIn:" + bookingDetails.booking.CheckIn.ToString() + "\n";
            invoice += "CheckOut:" + bookingDetails.booking.CheckOut.ToString() + "\n";

            double totalPrice = 0;
            double days = (bookingDetails.booking.CheckOut - bookingDetails.booking.CheckIn).TotalDays;

            foreach (var room in bookingDetails.rooms)
            {
                totalPrice += room.Rate * days;
                invoice += room.RoomNumber + ". " + room.Type + " $" + (room.Rate*days).ToString() +"\n";
            }

            invoice += "Total: $" + totalPrice.ToString();
            return invoice;
        }
    }
}