﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Models
{
    public interface BookingInvoice
    {
        string export(BookingDetails bookingDetails);
    }
}
