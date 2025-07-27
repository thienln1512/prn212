using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IBookingDetailService
    {
        void AddBookingDetail(BookingDetail bookingDetail);
        void UpdateBookingDetail(BookingDetail bookingDetail);
        void DeleteBookingDetail(int bookingDetailId);
        List<BookingDetail> GetBookingDetails();
        BookingDetail? GetBookingDetail(int id);
    }
}
