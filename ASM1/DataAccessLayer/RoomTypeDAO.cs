using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelObjects;

namespace DataAccessLayer
{
    public class RoomTypeDAO
    {
        private static List<RoomType> types = new()
        {
            new RoomType { RoomTypeID = 1, RoomTypeName = "Deluxe", TypeDescription = "Luxury Room", TypeNote = "" },
            new RoomType { RoomTypeID = 2, RoomTypeName = "Standard", TypeDescription = "Basic Room", TypeNote = "" }
        };

        public static List<RoomType> GetRoomTypes() => types;
    }

}
