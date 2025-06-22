using System.Collections.ObjectModel;
using System.Windows.Input;
using HotelObjects;
using Services;

namespace WPFApp.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        private readonly ICustomerService _service;

        public ObservableCollection<Customer> Customers { get; set; }
        public Customer SelectedCustomer { get; set; }

        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public CustomerViewModel(ICustomerService service)
        {
            _service = service;
            Customers = new ObservableCollection<Customer>(_service.GetCustomers());

            DeleteCommand = new RelayCommand(Delete, () => SelectedCustomer != null);
            EditCommand = new RelayCommand(Edit, () => SelectedCustomer != null);
        }

        private void Delete()
        {
            if (SelectedCustomer != null)
            {
                _service.RemoveCustomer(SelectedCustomer.CustomerID);
                Customers.Remove(SelectedCustomer);
            }
        }

        private void Edit()
        {
            if (SelectedCustomer != null)
            {
                _service.EditCustomer(SelectedCustomer);
                // Refresh UI
                int index = Customers.IndexOf(SelectedCustomer);
                if (index >= 0)
                {
                    Customers[index] = SelectedCustomer;
                }
            }
        }

        public void AddCustomer(Customer customer)
        {
            _service.CreateCustomer(customer);
            Customers.Add(customer);
        }

        public void EditCustomer(Customer customer)
        {
            _service.EditCustomer(customer);
            int index = Customers.IndexOf(SelectedCustomer);
            if (index >= 0)
            {
                Customers[index] = customer;
            }
        }
    }
}
