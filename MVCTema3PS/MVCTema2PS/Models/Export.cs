using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTema2PS.Models
{
    public interface Export
    {

        string export(List<Ticket> tickets);

    }
}
