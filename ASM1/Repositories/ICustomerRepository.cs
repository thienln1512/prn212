using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelObjects;

namespace Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        void Add(Customer customer);
        void Delete(int id);
        void Update(Customer customer);
    }
}
