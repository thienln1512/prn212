using BusinessObject;
using Repository;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService()
        {
            customerRepository = new CustomerRepository();
        }

        public void AddCustomer(Customer customer) => customerRepository.AddCustomer(customer);

        public void DeleteCustomer(int id) => customerRepository.DeleteCustomer(id);

        public Customer? GetCustomer(string email) => customerRepository.GetCustomerByEmail(email);

        public List<Customer> GetCustomers() => customerRepository.GetCustomers();

        public void UpdateCustomer(Customer customer) => customerRepository.UpdateCustomer(customer);
    }
}
