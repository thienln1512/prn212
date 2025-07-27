using BusinessObject;
using DataAccessLayer;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(Customer customer) => CustomerDAO.AddCustomer(customer);

        public void DeleteCustomer(int id) => CustomerDAO.DeleteCustomer(id);

        public Customer? GetCustomerByEmail(string email) => CustomerDAO.GetCustomerByEmail(email);

        public List<Customer> GetCustomers() => CustomerDAO.GetAllCustomers();

        public void UpdateCustomer(Customer customer) => CustomerDAO.UpdateCustomer(customer);
    }
}
