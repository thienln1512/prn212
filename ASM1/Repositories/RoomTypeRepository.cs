using System.Collections.Generic;
using HotelObjects;
using DataAccessLayer;

namespace Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public List<RoomType> GetAll() => RoomTypeDAO.GetRoomTypes();
    }
}
