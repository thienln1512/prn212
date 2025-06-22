using HotelObjects;

public class RoomDAO
{
    private static List<Room> rooms = new()
        {
            new Room { RoomID = 1, RoomNumber = "101", RoomDescription = "Sea view", RoomMaxCapacity = 2, RoomPricePerDate = 1000000, RoomStatus = 1, RoomTypeID = 1 },
            new Room { RoomID = 2, RoomNumber = "102", RoomDescription = "City view", RoomMaxCapacity = 3, RoomPricePerDate = 750000, RoomStatus = 1, RoomTypeID = 2 }
        };

    public static List<Room> GetRooms() => rooms;
    public static void AddRoom(Room r) => rooms.Add(r);
    public static void DeleteRoom(int id) => rooms.RemoveAll(r => r.RoomID == id);
    public static void UpdateRoom(Room r)
    {
        var index = rooms.FindIndex(x => x.RoomID == r.RoomID);
        if (index >= 0) rooms[index] = r;
    }
}
