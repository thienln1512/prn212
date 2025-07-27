using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BookingDetailService : IBookingDetailService
    {
        private readonly  IBookingDetailRepository bookingDetailRepository;

        public BookingDetailService()
        {
            bookingDetailRepository = new BookingDetailRepository();
        }

        public void AddBookingDetail(BookingDetail bookingDetail) => bookingDetailRepository.InsertBookingDetail(bookingDetail);

        public void DeleteBookingDetail(int bookingDetailId) => bookingDetailRepository.DeleteBookingDetail(bookingDetailId);

        public BookingDetail? GetBookingDetail(int id) => bookingDetailRepository.GetBookingDetailByID(id);

        public List<BookingDetail> GetBookingDetails() => bookingDetailRepository.GetBookingDetails();

        public void UpdateBookingDetail(BookingDetail bookingDetail) => bookingDetailRepository.UpdateBookingDetail(bookingDetail);
    }
}
