using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTema2PS.Models
{
    public class ExportJSON: Export
    {
        public string export(List<Ticket> tickets)
        {
            string json = JsonConvert.SerializeObject(tickets.ToArray());
            return json;
        }


    }
}