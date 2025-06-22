using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelObjects;
using Repositories;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public List<Customer> GetCustomers() => _repository.GetAll();
        public void CreateCustomer(Customer customer) => _repository.Add(customer);
        public void RemoveCustomer(int id) => _repository.Delete(id);
        public void EditCustomer(Customer customer) => _repository.Update(customer);
    }
}
