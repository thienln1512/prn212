using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BookingReservationService : IBookingReservationService
    {
        private readonly IBookingReservationRepository reservationRepository;

        public BookingReservationService()
        {
            reservationRepository = new BookingReservationRepository();
        }

        public void AddBookingReservation(BookingReservation reservation) => reservationRepository.AddBookingReservation(reservation);

        public void DeleteBookingReservation(int id) => reservationRepository.RemoveBookingReservation(id);

        public BookingReservation? GetBookingReservation(int id) => reservationRepository.GetBookingReservationById(id);

        public List<BookingReservation> GetBookingReservations() => reservationRepository.GetBookingReservations();

        public void UpdateBookingReservation(BookingReservation reservation) => reservationRepository.UpdateBookingReservation(reservation);
    }
}
