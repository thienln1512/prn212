using System.Windows;
using Services;
using HotelObjects;
using WPFApp.ViewModels;
using Repositories;

namespace WPFApp.Views
{
    public partial class ManageRoomsWindow : Window
    {
        private RoomViewModel _vm;

        public ManageRoomsWindow()
        {
            InitializeComponent();
            _vm = new RoomViewModel(new RoomService(new RoomRepository()), new RoomTypeService(new RoomTypeRepository()));
            DataContext = _vm;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new RoomDialog(_vm.RoomTypes);
            if (dialog.ShowDialog() == true)
            {
                _vm.Rooms.Add(dialog.Room);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedRoom == null) return;
            var dialog = new RoomDialog(_vm.RoomTypes, _vm.SelectedRoom);
            if (dialog.ShowDialog() == true)
            {
                _vm.EditCommand.Execute(null);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            _vm.DeleteCommand.Execute(null);
        }
    }
}