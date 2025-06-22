using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelObjects;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        private static List<Customer> customers = new()
        {
            new Customer { CustomerID = 1, CustomerFullName = "Nguyen Van A", Telephone = "0912345678", EmailAddress = "a@example.com", CustomerBirthday = new DateTime(1990,1,1), CustomerStatus = 1, Password = "123" },
            new Customer { CustomerID = 2, CustomerFullName = "Tran Thi B", Telephone = "0987654321", EmailAddress = "b@example.com", CustomerBirthday = new DateTime(1995,5,5), CustomerStatus = 1, Password = "123" }
        };

        public static List<Customer> GetCustomers() => customers;
        public static void AddCustomer(Customer c) => customers.Add(c);
        public static void DeleteCustomer(int id) => customers.RemoveAll(c => c.CustomerID == id);
        public static void UpdateCustomer(Customer c)
        {
            var index = customers.FindIndex(x => x.CustomerID == c.CustomerID);
            if (index >= 0) customers[index] = c;
        }
    }

}
