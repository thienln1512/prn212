using System.Windows;

namespace WPFApp.Views
{
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
        }

        private void ManageCustomers_Click(object sender, RoutedEventArgs e)
        {
            new ManageCustomersWindow().ShowDialog();
        }

        private void ManageRooms_Click(object sender, RoutedEventArgs e)
        {
            new ManageRoomsWindow().ShowDialog();
        }

        private void ViewReport_Click(object sender, RoutedEventArgs e)
        {
            new ReportWindow().ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }
    }
}