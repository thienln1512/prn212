using System.Collections.ObjectModel;
using System.Windows.Input;
using HotelObjects;
using Services;

namespace WPFApp.ViewModels
{
    public class RoomViewModel : BaseViewModel
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;

        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<RoomType> RoomTypes { get; set; }

        public Room SelectedRoom { get; set; }

        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public RoomViewModel(IRoomService roomService, IRoomTypeService roomTypeService)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;

            Rooms = new ObservableCollection<Room>(_roomService.GetAll());
            RoomTypes = new ObservableCollection<RoomType>(_roomTypeService.GetAll());

            DeleteCommand = new RelayCommand(Delete, () => SelectedRoom != null);
            EditCommand = new RelayCommand(Edit, () => SelectedRoom != null);
        }

        private void Delete()
        {
            if (SelectedRoom != null)
            {
                _roomService.Delete(SelectedRoom.RoomID);
                Rooms.Remove(SelectedRoom);
            }
        }

        private void Edit()
        {
            if (SelectedRoom != null)
            {
                _roomService.Update(SelectedRoom);
                int index = Rooms.IndexOf(SelectedRoom);
                if (index >= 0)
                {
                    Rooms[index] = SelectedRoom;
                }
            }
        }

        public void AddRoom(Room room)
        {
            _roomService.Add(room);
            Rooms.Add(room);
        }
    }
}
