using System.Collections.Generic;
using HotelObjects;

namespace Services
{
    public interface IRoomService
    {
        List<Room> GetAll();
        void Add(Room room);
        void Update(Room room);
        void Delete(int id);
    }
}