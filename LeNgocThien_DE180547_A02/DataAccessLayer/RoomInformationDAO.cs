using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoomInformationDAO
    {
        public static List<RoomInformation> GetRoomInformations()
        {
            using var context = new FuminiHotelManagementContext();
            return context.RoomInformations.Include(r => r.RoomType).Include(r => r.BookingDetails).ToList();
        }

        public static RoomInformation? GetRoomInformationByID(int roomID)
        {
            using var context = new FuminiHotelManagementContext();
            return context.RoomInformations.FirstOrDefault(roomInformation => roomInformation.RoomId == roomID);
        }

        public static void AddRoomInformation(RoomInformation roomInformation)
        {
            using var context = new FuminiHotelManagementContext();
            context.RoomInformations.Add(roomInformation);
            context.SaveChanges();
        }

        public static void UpdateRoomInformation(RoomInformation roomInformation)
        {
            using var context = new FuminiHotelManagementContext();
            RoomInformation? existingRoomInformation = context.RoomInformations.FirstOrDefault(room => room.RoomId == roomInformation.RoomId);
            if (existingRoomInformation != null)
            {
                existingRoomInformation.RoomNumber = roomInformation.RoomNumber;
                existingRoomInformation.RoomDetailDescription = roomInformation.RoomDetailDescription;
                existingRoomInformation.RoomMaxCapacity = roomInformation.RoomMaxCapacity;
                existingRoomInformation.RoomStatus = roomInformation.RoomStatus;
                existingRoomInformation.RoomPricePerDay = roomInformation.RoomPricePerDay;
                existingRoomInformation.RoomTypeId = roomInformation.RoomTypeId;

                context.SaveChanges();
            }
        }

        public static void DeleteRoomInformation(int roomID)
        {
            using var context = new FuminiHotelManagementContext();
            RoomInformation? existingRoomInformation = context.RoomInformations.FirstOrDefault(room => room.RoomId == roomID);
            if (existingRoomInformation != null)
            {
                existingRoomInformation.RoomStatus = 2;
                context.SaveChanges();
            }
        }
    }
}
