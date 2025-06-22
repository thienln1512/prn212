using System.Collections.Generic;
using HotelObjects;

namespace Services
{
    public interface IRoomTypeService
    {
        List<RoomType> GetAll();
    }
}