using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoomInformationRepository : IRoomInformationRepository
    {
        public void AddRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.AddRoomInformation(roomInformation);

        public void DeleteRoomInformation(int id) => RoomInformationDAO.DeleteRoomInformation(id);

        public List<RoomInformation> GetAllRoomInformation()  => RoomInformationDAO.GetRoomInformations();

        public RoomInformation? GetRoomInformationById(int id) => RoomInformationDAO.GetRoomInformationByID(id);

        public void UpdateRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.UpdateRoomInformation(roomInformation);
    }
}
