using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelObjects;

namespace Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
        void CreateCustomer(Customer customer);
        void RemoveCustomer(int id);
        void EditCustomer(Customer customer);
    }
}
