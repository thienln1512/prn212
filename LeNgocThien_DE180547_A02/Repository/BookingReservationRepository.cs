using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public void AddBookingReservation(BookingReservation BookingReservation) => BookingReservationDAO.AddReservation(BookingReservation);

        public BookingReservation? GetBookingReservationById(int id) => BookingReservationDAO.GetReservationById(id);

        public List<BookingReservation> GetBookingReservations() => BookingReservationDAO.GetReservations();

        public void RemoveBookingReservation(int id) => BookingReservationDAO.RemoveReservation(id);

        public void UpdateBookingReservation(BookingReservation BookingReservation) => BookingReservationDAO.UpdateReservation(BookingReservation);
    }
}
