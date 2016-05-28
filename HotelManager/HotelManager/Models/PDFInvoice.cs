using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace HotelManager.Models
{
    public class PDFInvoice: BookingInvoice
    {
        public string export(BookingDetails bookingDetails)
        {
            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Hotel Management";
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Times New Roman", 12, XFontStyle.Regular);
            XRect rect = new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point);
 
            double fontHeight = 16;
            double y = 0;

            // Create a rectangle to draw the text in and draw in it
            rect = new XRect(0, y, pdfPage.Width, fontHeight);
            graph.DrawString("HotelManagement",font, XBrushes.Black, rect, XStringFormats.TopCenter);
            y += fontHeight;

            rect = new XRect(0, y, pdfPage.Width, fontHeight);
            graph.DrawString("Client Details:", font, XBrushes.Black, rect, XStringFormats.TopLeft);
            y += fontHeight;

            rect = new XRect(0, y, pdfPage.Width, fontHeight);
            graph.DrawString("First Name:" + bookingDetails.client.FirstName, font, XBrushes.Black, rect, XStringFormats.TopLeft);
            y += fontHeight;

            rect = new XRect(0, y, pdfPage.Width, fontHeight);
            graph.DrawString("Last Name:" + bookingDetails.client.LastName, font, XBrushes.Black, rect, XStringFormats.TopLeft);
            y += fontHeight;

            rect = new XRect(0, y, pdfPage.Width, fontHeight);
            graph.DrawString("Email:" + bookingDetails.client.Email, font, XBrushes.Black, rect, XStringFormats.TopLeft);
            y += fontHeight;

            rect = new XRect(0, y, pdfPage.Width, fontHeight);
            graph.DrawString("Mobile Number:" + bookingDetails.client.MobileNumber, font, XBrushes.Black, rect, XStringFormats.TopLeft);
            y += fontHeight;
            y += fontHeight;
            
            rect = new XRect(0, y, pdfPage.Width, fontHeight);
            graph.DrawString("Booking Details:" + bookingDetails.client.LastName, font, XBrushes.Black, rect, XStringFormats.TopLeft);
            y += fontHeight;

            rect = new XRect(0, y, pdfPage.Width, fontHeight);
            graph.DrawString("CheckIn:" + bookingDetails.booking.CheckIn.ToString(), font, XBrushes.Black, rect, XStringFormats.TopLeft);
            y += fontHeight;

            rect = new XRect(0, y, pdfPage.Width, fontHeight);
            graph.DrawString("CheckOut:" + bookingDetails.booking.CheckOut.ToString(), font, XBrushes.Black, rect, XStringFormats.TopLeft);
            y += fontHeight;

            rect = new XRect(0, y, pdfPage.Width, fontHeight);
            graph.DrawString("Rooms:", font, XBrushes.Black, rect, XStringFormats.TopLeft);
            y += fontHeight;


            int nr_days =(int)(bookingDetails.booking.CheckOut - bookingDetails.booking.CheckIn).TotalDays;
            int totalPrice =0;
            foreach (var room in bookingDetails.rooms)
            {
                
                totalPrice += nr_days* room.Rate;
                rect = new XRect(0, y, pdfPage.Width, fontHeight);
                string str_room = room.RoomNumber.ToString() + ". " + room.Type + "  Price: $" + (nr_days* room.Rate).ToString(); 
                graph.DrawString(str_room , font, XBrushes.Black, rect, XStringFormats.TopLeft);
                y += fontHeight;
            }


            rect = new XRect(0, y, pdfPage.Width, fontHeight);
            graph.DrawString("Total: $" + totalPrice.ToString() , font, XBrushes.Black, rect, XStringFormats.TopLeft);
            y += fontHeight;

            string pdfFilename = "C:/invoice.pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);

            return pdfFilename;
        }
    }
}