using System.Collections.ObjectModel;
using System.Windows;
using HotelObjects;

namespace WPFApp.Views
{
    public partial class RoomDialog : Window
    {
        public Room Room { get; set; }
        public ObservableCollection<RoomType> RoomTypes { get; set; }
        public RoomType SelectedRoomType { get; set; }

        public RoomDialog(ObservableCollection<RoomType> roomTypes)
        {
            InitializeComponent();
            Room = new Room();
            RoomTypes = roomTypes;
            DataContext = this;
        }

        public RoomDialog(ObservableCollection<RoomType> roomTypes, Room existing)
        {
            InitializeComponent();
            Room = new Room
            {
                RoomID = existing.RoomID,
                RoomNumber = existing.RoomNumber,
                RoomDescription = existing.RoomDescription,
                RoomMaxCapacity = existing.RoomMaxCapacity,
                RoomPricePerDate = existing.RoomPricePerDate,
                RoomTypeID = existing.RoomTypeID,
                RoomStatus = existing.RoomStatus
            };
            RoomTypes = roomTypes;
            SelectedRoomType = roomTypes.FirstOrDefault(t => t.RoomTypeID == Room.RoomTypeID);
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRoomType != null)
                Room.RoomTypeID = SelectedRoomType.RoomTypeID;
            DialogResult = true;
            Close();
        }
    }
}