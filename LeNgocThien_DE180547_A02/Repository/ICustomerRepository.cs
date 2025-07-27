using BusinessObject;

namespace Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomers();
        Customer? GetCustomerByEmail(string email);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}
