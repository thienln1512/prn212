using BusinessObject;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LeNgocThienWPF
{
    /// <summary>
    /// Interaction logic for ReservationHistoryWindow.xaml
    /// </summary>
    public partial class ReservationHistoryWindow : Window
    {
        private Customer _customer;
        private readonly IBookingReservationService reservationService = new BookingReservationService();
        private readonly ICustomerService customerService = new CustomerService();
        private readonly IRoomInformationService roomInformationService = new RoomInformationService();

        public List<BookingReservation> Reservations { get; set; }

        public ReservationHistoryWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            LoadReservations();
            btnProfile.Content = _customer.CustomerFullName;
            DataContext = this;
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            var profileWindow = new ProfileWindow(_customer);

            if (profileWindow.ShowDialog() == true)
            {
                _customer = customerService.GetCustomer(_customer.EmailAddress)!;
                btnProfile.Content = _customer.CustomerFullName;
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void txtSearchRoomNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearchRoomNumber.Text.Trim();
            dgReservations.ItemsSource = Reservations.Where(r => r.CustomerId == _customer.CustomerId && r.BookingDetails.ElementAt(0).Room.RoomNumber.Contains(searchText)).ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var bookingWindow = new BookingWindow(_customer);

            if (bookingWindow.ShowDialog() == true)
            {
                LoadReservations();
            }
        }

        private void LoadReservations()
        {
            Reservations = reservationService.GetBookingReservations().Where(r => r.CustomerId == _customer.CustomerId).ToList();

            dgReservations.ItemsSource = null;
            dgReservations.ItemsSource = Reservations;
        }

        private void dgReservations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgReservations.SelectedItem is BookingReservation selectedReservation)
            {
                var bookingWindow = new BookingWindow(selectedReservation);
                bookingWindow.ShowDialog();
            }
        }
    }
}
