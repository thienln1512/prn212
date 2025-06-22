using System.Collections.Generic;
using HotelObjects;
using Repositories;

namespace Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _repo;

        public RoomTypeService(IRoomTypeRepository repo)
        {
            _repo = repo;
        }

        public List<RoomType> GetAll() => _repo.GetAll();
    }
}