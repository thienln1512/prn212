using System.Windows;
using HotelObjects;
using Repositories;
using Services;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    public partial class ManageCustomersWindow : Window
    {
        private CustomerViewModel _vm;

        public ManageCustomersWindow()
        {
            InitializeComponent();
            _vm = new CustomerViewModel(new CustomerService(new CustomerRepository()));
            DataContext = _vm;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CustomerDialog();
            if (dialog.ShowDialog() == true)
            {
                _vm.Customers.Add(dialog.Customer);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SelectedCustomer == null) return;

            var dialog = new CustomerDialog(_vm.SelectedCustomer);
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
