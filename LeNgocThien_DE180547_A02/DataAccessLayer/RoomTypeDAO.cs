using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class RoomTypeDAO
    {
        public static List<RoomType> GetRoomTypes()
        {
            using var context = new FuminiHotelManagementContext();
            return context.RoomTypes.Include(rt => rt.RoomInformations).ToList();
        }

        public static RoomType? GetRoomTypeByID(int roomTypeID)
        {
            using var context = new FuminiHotelManagementContext();
            return context.RoomTypes.FirstOrDefault(roomType => roomType.RoomTypeId == roomTypeID);
        }

        public static void AddRoomType(RoomType roomType)
        { 
            using var context = new FuminiHotelManagementContext();
            context.RoomTypes.Add(roomType);
            context.SaveChanges();
        }

        public static void UpdateRoomType(RoomType roomType)
        {
            using var context = new FuminiHotelManagementContext();
            RoomType? existingRoomType = context.RoomTypes.FirstOrDefault(rt => rt.RoomTypeId == roomType.RoomTypeId);
            if (existingRoomType != null)
            {
                existingRoomType.RoomTypeName = roomType.RoomTypeName;
                existingRoomType.TypeDescription = roomType.TypeDescription;
                existingRoomType.TypeNote = roomType.TypeNote;

                context.SaveChanges();
            }
        }

        public static void DeleteRoomType(int roomTypeID)
        {
            using var context = new FuminiHotelManagementContext();
            RoomType? existingRoomType = context.RoomTypes.FirstOrDefault(rt => rt.RoomTypeId == roomTypeID);
            if (existingRoomType != null)
            {
                context.RoomTypes.Remove(existingRoomType);
                context.SaveChanges();
            }
        }
    }
}
