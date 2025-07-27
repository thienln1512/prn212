using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BookingDetailDAO
    {
        public static List<BookingDetail> GetBookingDetails()
        {
            using var context = new FuminiHotelManagementContext();
           
            return context.BookingDetails.Include(bd => bd.BookingReservation).Include(bd => bd.Room).ToList();
        }

        public static void AddBookingDetail(BookingDetail bookingDetail)
        {
            using var context = new FuminiHotelManagementContext();
            context.BookingDetails.Add(bookingDetail);
            context.SaveChanges();
        }

        public static void RemoveBookingDetail(int id)
        {
            using var context = new FuminiHotelManagementContext();
            var bd = context.BookingDetails.FirstOrDefault(bd => bd.BookingReservationId == id);

            if (bd != null) context.BookingDetails.Remove(bd);

            context.SaveChanges();
        }

        public static void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            using var context = new FuminiHotelManagementContext();
            var bd = context.BookingDetails.FirstOrDefault(bd => bd.BookingReservationId == bookingDetail.BookingReservationId);

            if (bd != null)
            {
                bd.RoomId = bookingDetail.RoomId;
                bd.StartDate = bookingDetail.StartDate;
                bd.EndDate = bookingDetail.EndDate;
                bd.ActualPrice = bookingDetail.ActualPrice;
            }

            context.SaveChanges();
        }

        public static BookingDetail? GetBookingDetail(int id)
        {
            using var context = new FuminiHotelManagementContext();
            return context.BookingDetails.FirstOrDefault(bd => bd.BookingReservationId == id);
        }
    }
}
