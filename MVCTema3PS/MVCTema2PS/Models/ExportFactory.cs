using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTema2PS.Models
{
    public class ExportFactory
    {

        public enum ExportTypes { CSV, JSON };

        private static ExportFactory exportFactory = new ExportFactory();

        public static ExportFactory getInstance()
        {
            return exportFactory;
        }


        public Export export(ExportTypes exportType)
        {
            Export export = null;
            if (exportType.Equals(ExportTypes.CSV))
            {
                export = new ExportCSV();
            }
            else if (exportType.Equals(ExportTypes.JSON))
            {
                export = new ExportJSON();
            }
            return export;
        }



    }
}