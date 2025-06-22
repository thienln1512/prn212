using System.Windows.Input;
using System.Windows;
using WPFApp;
using WPFApp.Views;
using System.Linq;
using Repositories;
using Services;

namespace WPFApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            var adminEmail = App.Configuration["AdminAccount:Email"];
            var adminPass = App.Configuration["AdminAccount:Password"];

            if (Email == adminEmail && Password == adminPass)
            {
                var adminWindow = new AdminMainWindow();
                adminWindow.Show();

                Application.Current.Windows
                    .OfType<Window>()
                    .FirstOrDefault(w => w is LoginWindow)
                    ?.Close();
            }
            else
            {
                var customerService = new CustomerService(new CustomerRepository());
                var customer = customerService.GetCustomers()
                                  .FirstOrDefault(c => c.EmailAddress == Email
                                                    && c.Password == Password
                                                    && c.CustomerStatus == 1);

                if (customer != null)
                {
                    var customerWindow = new CustomerMainWindow(customer);
                    customerWindow.Show();

                    Application.Current.Windows
                        .OfType<Window>()
                        .FirstOrDefault(w => w is LoginWindow)
                        ?.Close();
                }
                else
                {
                    MessageBox.Show("Login failed.");
                }
            }
        }

    }
}
