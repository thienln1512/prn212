using BusinessObject;
using Service;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        private Customer _customer;
        private readonly IRoomInformationService roomInformationService = new RoomInformationService();
        private readonly IBookingReservationService reservationService = new BookingReservationService();
        private readonly IBookingDetailService bookingDetailService = new BookingDetailService();


        public BookingWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            DataContext = this;
            LoadRoomList();
        }

        public BookingWindow(BookingReservation reservation)
        {
            InitializeComponent();
            DataContext = this;
            LoadRoomList();
            btnBook.Visibility = Visibility.Hidden;
            cboRoomNumber.SelectedValue = reservation.BookingDetails.ElementAt(0).RoomId;
            dptbCheckIn.SelectedDate = reservation.BookingDetails.ElementAt(0).StartDate.ToDateTime(new TimeOnly(0, 0));
            dptbCheckOut.SelectedDate = reservation.BookingDetails.ElementAt(0).EndDate.ToDateTime(new TimeOnly(0, 0));
            txtActualPrice.Text = reservation.BookingDetails.ElementAt(0).ActualPrice.ToString();
            txtTotalPrice.Text = reservation.TotalPrice.ToString();
        }

        private void LoadRoomList()
        {
            cboRoomNumber.ItemsSource = roomInformationService.GetRoomInformation().Where(ri => ri.RoomStatus == 1);
            cboRoomNumber.DisplayMemberPath = "RoomNumber";
            cboRoomNumber.SelectedValuePath = "RoomId";
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            if (cboRoomNumber.SelectedItem is RoomInformation room && dptbCheckIn.SelectedDate.HasValue && dptbCheckOut.SelectedDate.HasValue && decimal.Parse(txtTotalPrice.Text) > 0)
            {
                var reservation = new BookingReservation
                {
                    BookingDate = DateOnly.FromDateTime(DateTime.Now),
                    CustomerId = _customer.CustomerId,
                    BookingStatus = 1,
                    TotalPrice = decimal.Parse(txtTotalPrice.Text)
                };

                var bookingDetail = new BookingDetail
                {
                    BookingReservationId = reservation.BookingReservationId,
                    RoomId = room.RoomId,
                    StartDate = DateOnly.FromDateTime(dptbCheckIn.SelectedDate.Value),
                    EndDate = DateOnly.FromDateTime(dptbCheckOut.SelectedDate.Value),
                    ActualPrice = decimal.Parse(txtActualPrice.Text)
                };

                reservationService.AddBookingReservation(reservation);
                bookingDetailService.AddBookingDetail(bookingDetail);
                DialogResult = true;

                Close();
            }
            else
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }

        private void RecaculateTotalPrice(object sender, EventArgs e)
        {
            if (cboRoomNumber.SelectedItem is RoomInformation room && dptbCheckIn.SelectedDate.HasValue && dptbCheckOut.SelectedDate.HasValue)
            {
                var days = (decimal)(dptbCheckOut.SelectedDate.Value - dptbCheckIn.SelectedDate.Value).TotalDays;
                txtActualPrice.Text = (days * room.RoomPricePerDay).ToString();
                txtTotalPrice.Text = (days * room.RoomPricePerDay * 1.1m).ToString();
            }
        }
    }
}
