using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IRoomInformationService
    {
        List<RoomInformation> GetRoomInformation();
        RoomInformation? GetRoomInformationById(int id);
        void AddRoomInformation(RoomInformation roomInformation);    
        void UpdateRoomInformation(RoomInformation roomInformation);
        void DeleteRoomInformation(int id);

    }
}
