using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelObjects
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public DateTime BookingDate { get; set; }
        public int RoomID { get; set; }
        public decimal TotalAmount { get; set; }
    }

}
