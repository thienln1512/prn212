using System.Windows;
using HotelObjects;

namespace WPFApp.Views
{
    public partial class EditProfileWindow : Window
    {
        public Customer UpdatedCustomer { get; set; }

        public EditProfileWindow(Customer customer)
        {
            InitializeComponent();
            UpdatedCustomer = new Customer
            {
                CustomerID = customer.CustomerID,
                CustomerFullName = customer.CustomerFullName,
                Telephone = customer.Telephone,
                EmailAddress = customer.EmailAddress,
                CustomerBirthday = customer.CustomerBirthday,
                CustomerStatus = customer.CustomerStatus,
                Password = customer.Password
            };
            DataContext = UpdatedCustomer;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
