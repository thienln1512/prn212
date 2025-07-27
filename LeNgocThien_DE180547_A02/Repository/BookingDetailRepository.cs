using BusinessObject;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        public void DeleteBookingDetail(int bookingDetailID) => BookingDetailDAO.RemoveBookingDetail(bookingDetailID);

        public BookingDetail? GetBookingDetailByID(int bookingDetailId) => BookingDetailDAO.GetBookingDetail(bookingDetailId);

        public List<BookingDetail> GetBookingDetails() => BookingDetailDAO.GetBookingDetails();

        public void InsertBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.AddBookingDetail(bookingDetail);

        public void UpdateBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.UpdateBookingDetail(bookingDetail);
    }
}
