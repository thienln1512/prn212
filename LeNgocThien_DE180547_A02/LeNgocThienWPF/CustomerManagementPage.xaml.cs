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
    /// Interaction logic for CustomerManagementPage.xaml
    /// </summary>
    public partial class CustomerManagementPage : Page
    {
        private readonly ICustomerService customerService = new CustomerService();
        private readonly IBookingReservationService reservationService = new BookingReservationService();
        public List<Customer> Customers { get; set; }

        public CustomerManagementPage()
        {
            InitializeComponent();
            LoadCustomers();
        }

        private void txtSearchCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearchCustomer.Text;
            dgCustomers.ItemsSource = Customers.Where(c => c.CustomerFullName!.ToLower().Contains(searchText)).ToList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedCustomers = dgCustomers.SelectedItems.Cast<Customer>().ToList();

            if (selectedCustomers.Count > 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete the selected customer(s)?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    foreach (var customer in selectedCustomers)
                    {
                        customerService.DeleteCustomer(customer.CustomerId);
                    }
                }
                else
                {
                    MessageBox.Show("Please select at least one customer to delete.", "Selection Error");
                }
            }

            LoadCustomers();
        }

        private void LoadCustomers()
        {
            Customers = customerService.GetCustomers();
            foreach (var customer in Customers)
            {
                customer.BookingReservations = reservationService.GetBookingReservations().Where(r => r.CustomerId == customer.CustomerId).ToList();
            }
            dgCustomers.ItemsSource = null;
            dgCustomers.ItemsSource = Customers;
        }

        private void dgCustomers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgCustomers.SelectedItem is Customer selectedCustomer)
            {
                var profileWindow = new ProfileWindow(selectedCustomer);
                if (profileWindow.ShowDialog() == true)
                {
                    LoadCustomers();
                }
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var profileWindow = new ProfileWindow();
            if (profileWindow.ShowDialog() == true)
            {
                LoadCustomers();
            }
        }
    }
}
