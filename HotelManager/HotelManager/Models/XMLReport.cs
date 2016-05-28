using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace HotelManager.Models
{
    public class XMLReport : BookingReport
    {
        public string exportReport(List<BookingDetails> bookingsDetails)
        {

            var xEle = new XElement("Bookings",
                                from booking in bookingsDetails
                                    select new XElement("Booking",
                                                    new XElement("BookingId", booking.booking.BookingId),
                                                    new XElement("CheckIn", booking.booking.CheckIn),
                                                    new XElement("CheckOut",booking.booking.CheckOut),
                                                    new XElement("ClientId", booking.client.ClientId),
                                                    new XElement("ClientFirstName", booking.client.FirstName),
                                                    new XElement("ClientLastName", booking.client.LastName),
                                                    new XElement("ClientEmail", booking.client.Email),
                                                    new XElement("ClientMobileNumber", booking.client.MobileNumber),
                                                    from room in booking.rooms 
                                                        select new XElement("Room",
                                                                    new XElement("RoomID", room.RoomId),
                                                                    new XElement("RoomNumber",room.RoomNumber),
                                                                    new XElement("RoomType", room.Type),
                                                                    new XElement("RoomRate", room.Rate)
                                                            )
                                                 )       
                                    );
            return xEle.ToString();


        }
    }
}