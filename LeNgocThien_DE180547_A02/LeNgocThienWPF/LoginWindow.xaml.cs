using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ICustomerService _customerService = new CustomerService();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var jsonString = File.ReadAllText("appsettings.json");
            using var doc = JsonDocument.Parse(jsonString);
            var root = doc.RootElement;

            var admin = root.GetProperty("admin");
            var adminEmail = admin.GetProperty("email").GetString();
            var adminPassword = admin.GetProperty("password").GetString();


            if (txtEmail.Text.Trim().Equals(adminEmail) && txtPassword.Password.Equals(adminPassword))
            {
                var managementWindow = new ManagementWindow();
                managementWindow.Show();
                Close();
            }
            else
            {
                var customer = _customerService.GetCustomer(txtEmail.Text);

                if (customer == null)
                {
                    MessageBox.Show("Email does not exist!", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (customer.Password!.Equals(txtPassword.Password))
                    {
                        var reservationHistoryWindow = new ReservationHistoryWindow(customer);
                        reservationHistoryWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password!", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) => Close();
    }
}
