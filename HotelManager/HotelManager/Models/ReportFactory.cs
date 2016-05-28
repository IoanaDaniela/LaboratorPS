using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManager.Models
{
    public class ReportFactory
    {
        public enum ReportTypes { CSV, XML };

        private static ReportFactory reportFactory = new ReportFactory();

        public static ReportFactory getInstance()
        {
            return reportFactory;
        }


        public BookingReport export(ReportTypes reportType)
        {
            BookingReport report = null;
            if (reportType.Equals(ReportTypes.CSV))
            {
                report = new CSVReport();
            }
            else if (reportType.Equals(ReportTypes.XML))
            {
                report = new XMLReport();
            }
            return report;
        }
    }
}