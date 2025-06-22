using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using HotelObjects;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetAll() => CustomerDAO.GetCustomers();
        public void Add(Customer customer) => CustomerDAO.AddCustomer(customer);
        public void Delete(int id) => CustomerDAO.DeleteCustomer(id);
        public void Update(Customer customer) => CustomerDAO.UpdateCustomer(customer);
    }
}
