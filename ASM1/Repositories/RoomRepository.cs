using System.Collections.Generic;
using HotelObjects;
using DataAccessLayer;

namespace Repositories
{
    public class RoomRepository : IRoomRepository
    {
        public List<Room> GetAll() => RoomDAO.GetRooms();
        public void Add(Room room) => RoomDAO.AddRoom(room);
        public void Delete(int id) => RoomDAO.DeleteRoom(id);
        public void Update(Room room) => RoomDAO.UpdateRoom(room);
    }
}