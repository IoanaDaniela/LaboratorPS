﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Models
{
    public interface BookingReport
    {
        string exportReport(List<BookingDetails> bookingsDetails);
    }
}
