using System.Windows;
using HotelObjects;

namespace WPFApp.Views
{
    public partial class CustomerDialog : Window
    {
        public Customer Customer { get; set; }

        public CustomerDialog()
        {
            InitializeComponent();
            Customer = new Customer();
            DataContext = Customer;
        }

        public CustomerDialog(Customer existing)
        {
            InitializeComponent();
            Customer = new Customer
            {
                CustomerID = existing.CustomerID,
                CustomerFullName = existing.CustomerFullName,
                Telephone = existing.Telephone,
                EmailAddress = existing.EmailAddress,
                CustomerBirthday = existing.CustomerBirthday,
                CustomerStatus = existing.CustomerStatus,
                Password = existing.Password
            };
            DataContext = Customer;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}