using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace HotelManager.Models
{
    public class CSVReport : BookingReport
    {
        public string exportReport(List<BookingDetails> bookingsDetails)
        {
            return Export(true, bookingsDetails);
        }


        public string Export(bool includeHeaderLine, List<BookingDetails> bookingdetails)
        {
            List<CSVModel> bookings = new List<CSVModel>();
    
            foreach (var bD in bookingdetails)
            {
                CSVModel csv = new CSVModel();
                csv.BookingId = bD.booking.BookingId;
                csv.CheckIn= bD.booking.CheckIn;
                csv.CheckOut= bD.booking.CheckOut;
                csv.ClientId= bD.client.ClientId;
                csv.FirstName = bD.client.FirstName;
                csv.LastName = bD.client.LastName;
                csv.Email = bD.client.Email;
                csv.MobileNumber = bD.client.MobileNumber;
                csv.RoomId = bD.rooms.First().RoomId;
                csv.RoomNumber = bD.rooms.First().RoomNumber;
                csv.Type = bD.rooms.First().Type;
                csv.Rate = bD.rooms.First().Rate;
                csv.TotalPrice = bD.totalPrice;
                bookings.Add(csv);
            }            

            StringBuilder sb = new StringBuilder();
            //Get properties using reflection.
            IList<PropertyInfo> propertyInfos = typeof(CSVModel).GetProperties();
            
            if (includeHeaderLine)
            {
                //add header line.
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    sb.Append(propertyInfo.Name).Append(",");
                }
                sb.Remove(sb.Length - 1, 1).AppendLine();
            }

            



            //add value for each property.
            foreach (CSVModel t in bookings)
            {
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    sb.Append(propertyInfo.GetValue(t, null)).Append(",");
                }
                sb.Remove(sb.Length - 1, 1).AppendLine();
            }


            


            return sb.ToString();
        }



    }
}