using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoomInformationService : IRoomInformationService
    {
        public void AddRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.AddRoomInformation(roomInformation);

        public void DeleteRoomInformation(int id) => RoomInformationDAO.DeleteRoomInformation(id);

        public List<RoomInformation> GetRoomInformation() => RoomInformationDAO.GetRoomInformations();

        public RoomInformation? GetRoomInformationById(int id) => RoomInformationDAO.GetRoomInformationByID(id);

        public void UpdateRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.UpdateRoomInformation(roomInformation);
    }
}
