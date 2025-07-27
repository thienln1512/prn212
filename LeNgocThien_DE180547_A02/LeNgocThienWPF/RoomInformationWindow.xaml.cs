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
    /// Interaction logic for RoomInformationWindow.xaml
    /// </summary>
    public partial class RoomInformationWindow : Window
    {
        private RoomInformation roomInformation;
        private readonly IRoomInformationService roomInformationService = new RoomInformationService();
        private readonly IRoomTypeService roomTypeService = new RoomTypeService();


        public RoomInformationWindow()
        {
            InitializeComponent();
            roomInformation = new RoomInformation
            {
                RoomStatus = 1
            };
            txtStatus.Text = "1";
            btnUpdate.Content = "Add";
            DataContext = roomInformation;

            txtRoomType.ItemsSource = roomTypeService.GetRoomTypes();
            txtRoomType.DisplayMemberPath = "RoomTypeName";
            txtRoomType.SelectedValuePath = "RoomTypeId";
        }

        public RoomInformationWindow(RoomInformation _roomInformation)
        {
            InitializeComponent();
            roomInformation = _roomInformation;
            DataContext = roomInformation;

            txtRoomType.ItemsSource = roomTypeService.GetRoomTypes();
            txtRoomType.DisplayMemberPath = "RoomTypeName"; 
            txtRoomType.SelectedValuePath = "RoomTypeId"; 
            txtRoomType.SelectedValue = roomInformation.RoomTypeId;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            roomInformation.RoomTypeId = (int)txtRoomType.SelectedValue;


            if (btnUpdate.Content.Equals("Add"))
            {
                roomInformationService.AddRoomInformation(roomInformation);
            }
            else
            {
                roomInformationService.UpdateRoomInformation(roomInformation);
            }

            DialogResult = true;
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }
    }
}
