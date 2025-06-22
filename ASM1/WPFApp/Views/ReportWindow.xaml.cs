using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using HotelObjects;

namespace WPFApp.Views
{
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            DateTime? start = StartDatePicker.SelectedDate;
            DateTime? end = EndDatePicker.SelectedDate;

            if (start == null || end == null || start > end)
            {
                MessageBox.Show("Please select a valid date range.");
                return;
            }

            // Dummy booking list for demonstration
            var bookings = new List<Booking>
            {
                new Booking { BookingID = 1, CustomerID = 1, RoomID = 1, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 3), Total = 500 },
                new Booking { BookingID = 2, CustomerID = 2, RoomID = 2, StartDate = new DateTime(2024, 2, 5), EndDate = new DateTime(2024, 2, 8), Total = 900 },
                new Booking { BookingID = 3, CustomerID = 1, RoomID = 3, StartDate = new DateTime(2024, 3, 10), EndDate = new DateTime(2024, 3, 12), Total = 400 },
            };

            var result = bookings.Where(b => b.StartDate >= start && b.EndDate <= end)
                                 .OrderByDescending(b => b.Total)
                                 .ToList();

            ReportGrid.ItemsSource = result;
        }
    }

    public class Booking
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int RoomID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Total { get; set; }
    }
}