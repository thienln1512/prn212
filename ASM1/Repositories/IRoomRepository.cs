using System.Collections.Generic;
using HotelObjects;

namespace Repositories
{
    public interface IRoomRepository
    {
        List<Room> GetAll();
        void Add(Room room);
        void Delete(int id);
        void Update(Room room);
    }
}