using HotelObjects;

public class BookingDAO
{
    private static List<Booking> bookings = new()
        {
            new Booking { BookingID = 1, CustomerID = 1, BookingDate = new DateTime(2025,6,12), RoomID = 1, TotalAmount = 1000000 },
            new Booking { BookingID = 2, CustomerID = 2, BookingDate = new DateTime(2025,6,17), RoomID = 2, TotalAmount = 750000 },
            new Booking { BookingID = 3, CustomerID = 1, BookingDate = new DateTime(2025,6,20), RoomID = 1, TotalAmount = 1500000 }
        };

    public static List<Booking> GetBookings() => bookings;
}
