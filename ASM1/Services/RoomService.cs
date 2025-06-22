using System.Collections.Generic;
using HotelObjects;
using Repositories;

namespace Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;

        public RoomService(IRoomRepository repo)
        {
            _repo = repo;
        }

        public List<Room> GetAll() => _repo.GetAll();
        public void Add(Room room) => _repo.Add(room);
        public void Update(Room room) => _repo.Update(room);
        public void Delete(int id) => _repo.Delete(id);
    }
}