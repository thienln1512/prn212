using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoomTypeService : IRoomTypeService
    {
        public void AddRoomType(RoomType roomType) => RoomTypeDAO.AddRoomType(roomType);

        public void DeleteRoomType(int id) => RoomTypeDAO.DeleteRoomType(id);

        public RoomType? GetRoomType(int id) => RoomTypeDAO.GetRoomTypeByID(id);

        public List<RoomType> GetRoomTypes() => RoomTypeDAO.GetRoomTypes();

        public void UpdateRoomType(RoomType roomType) => RoomTypeDAO.UpdateRoomType(roomType);
    }
}
