using BusinessObject;

namespace Repository
{
    public interface IBookingDetailRepository
    {
        List<BookingDetail> GetBookingDetails();
        BookingDetail? GetBookingDetailByID(int bookingDetailId);
        void InsertBookingDetail(BookingDetail bookingDetail);
        void DeleteBookingDetail(int bookingDetailID);
        void UpdateBookingDetail(BookingDetail bookingDetail);
    }
}
