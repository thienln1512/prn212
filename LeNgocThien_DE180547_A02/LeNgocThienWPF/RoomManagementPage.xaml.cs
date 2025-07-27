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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeNgocThienWPF
{
    /// <summary>
    /// Interaction logic for RoomManagementPage.xaml
    /// </summary>
    public partial class RoomManagementPage : Page
    {
        private readonly IRoomInformationService roomInformationService = new RoomInformationService();
        private readonly IBookingDetailService bookingDetailService = new BookingDetailService();
        public List<RoomInformation> RoomInformations { get; set; }

        public RoomManagementPage()
        {
            InitializeComponent();
            LoadRooms();
        }

        private void LoadRooms()
        {
            RoomInformations = roomInformationService.GetRoomInformation();
            dgRooms.ItemsSource = null;
            dgRooms.ItemsSource = RoomInformations;
        }

        private void txtSearchRoom_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearchRoom.Text;
            dgRooms.ItemsSource = RoomInformations.Where(r => r.RoomNumber.Contains(searchText)).ToList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedRooms = dgRooms.SelectedItems.Cast<RoomInformation>().ToList();
            var bookingDetails = bookingDetailService.GetBookingDetails();
            if (selectedRooms.Count > 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete the selected room(s)?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    foreach (var room in selectedRooms)
                    {
                        if (bookingDetails.FirstOrDefault(b => b.RoomId == room.RoomId && b.StartDate <= DateOnly.FromDateTime(DateTime.Now) &&
                                                                b.EndDate >= DateOnly.FromDateTime(DateTime.Now)) != null)
                        {
                            MessageBox.Show($"Room {room.RoomNumber} can't be deleted");
                        }
                        else
                        {
                            roomInformationService.DeleteRoomInformation(room.RoomId);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select at least one room to delete.", "Selection Error");
            }

            LoadRooms();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void dgRooms_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgRooms.SelectedItem is RoomInformation selectedRoom)
            {
                var roomInformationWindow = new RoomInformationWindow(selectedRoom);
                if (roomInformationWindow.ShowDialog() == true)
                {
                    LoadRooms();
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var roomInformationWindow = new RoomInformationWindow();
            if (roomInformationWindow.ShowDialog() == true)
            {
                LoadRooms();
            }
        }
    }
}
