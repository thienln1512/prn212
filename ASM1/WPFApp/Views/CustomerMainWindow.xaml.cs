using System.Windows;
using System.Collections.Generic;
using HotelObjects;

namespace WPFApp.Views
{
    public partial class CustomerMainWindow : Window
    {
        private Customer _loggedInCustomer;

        public CustomerMainWindow(Customer customer)
        {
            InitializeComponent();
            _loggedInCustomer = customer;
            CustomerNameText.Text = customer.CustomerFullName;

            // Dummy booking data for demonstration
            var bookings = new List<Booking>
            {
                new Booking { BookingID = 1, CustomerID = customer.CustomerID, RoomID = 1, StartDate = new DateTime(2024, 1, 1), EndDate = new DateTime(2024, 1, 3), Total = 500 },
                new Booking { BookingID = 3, CustomerID = customer.CustomerID, RoomID = 3, StartDate = new DateTime(2024, 3, 10), EndDate = new DateTime(2024, 3, 12), Total = 400 }
            };

            BookingHistoryGrid.ItemsSource = bookings;
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new EditProfileWindow(_loggedInCustomer);
            if (dialog.ShowDialog() == true)
            {
                _loggedInCustomer = dialog.UpdatedCustomer;
                CustomerNameText.Text = _loggedInCustomer.CustomerFullName;
            }
        }
    }
}