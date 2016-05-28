using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManager.Models
{
    public class InvoiceFactory
    {
        public enum InvoiceTypes { PDF, TXT };

        private static InvoiceFactory invoiceFactory = new InvoiceFactory();

        public static InvoiceFactory getInstance()
        {
            return invoiceFactory;
        }


        public BookingInvoice exportInvoice(InvoiceTypes invoiceType)
        {
            BookingInvoice invoice = null;
            if (invoiceType.Equals(InvoiceTypes.PDF))
            {
                invoice = new PDFInvoice();
            }
            else if (invoiceType.Equals(InvoiceTypes.TXT))
            {
                invoice = new TXTInvoice();
            }
            return invoice;
        }
    }
}