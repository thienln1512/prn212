using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public void AddRoomType(RoomType roomType) => RoomTypeDAO.AddRoomType(roomType);

        public void DeleteRoomType(int id) => RoomTypeDAO.DeleteRoomType(id);

        public RoomType? GetRoomTypeByID(int id) => RoomTypeDAO.GetRoomTypeByID(id);

        public List<RoomType> GetRoomTypes() => RoomTypeDAO.GetRoomTypes();

        public void UpdateRoomType(RoomType roomType) => RoomTypeDAO.UpdateRoomType(roomType);
    }
}
