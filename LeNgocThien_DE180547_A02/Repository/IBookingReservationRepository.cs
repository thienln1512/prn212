using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace Repository
{
    public interface IBookingReservationRepository
    {
        List<BookingReservation> GetBookingReservations();
        void AddBookingReservation(BookingReservation BookingReservation);
        void RemoveBookingReservation(int id);
        void UpdateBookingReservation(BookingReservation BookingReservation);
        BookingReservation? GetBookingReservationById(int id);
    }
}
