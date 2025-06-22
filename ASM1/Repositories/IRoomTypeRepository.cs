using System.Collections.Generic;
using HotelObjects;

namespace Repositories
{
    public interface IRoomTypeRepository
    {
        List<RoomType> GetAll();
    }
}