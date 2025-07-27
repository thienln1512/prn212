using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CustomerDAO
    {
        public static List<Customer> GetAllCustomers()
        {
            using var context = new FuminiHotelManagementContext();
            return context.Customers.Include(c => c.BookingReservations).ToList();
        }

        public static Customer? GetCustomerByEmail(string emailAddress)
        {
            using var context = new FuminiHotelManagementContext();
            return context.Customers.FirstOrDefault(c => c.EmailAddress == emailAddress);
        }

        public static void AddCustomer(Customer customer)
        {
            using var context = new FuminiHotelManagementContext();
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public static void UpdateCustomer(Customer customer)
        {
            using var context = new FuminiHotelManagementContext();
            Customer? existingCustomer = context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);

            if (existingCustomer != null)
            {
                existingCustomer.CustomerFullName = customer.CustomerFullName;
                existingCustomer.Telephone = customer.Telephone;
                existingCustomer.EmailAddress = customer.EmailAddress;
                existingCustomer.CustomerBirthday = customer.CustomerBirthday;
                existingCustomer.CustomerStatus = customer.CustomerStatus;
                existingCustomer.Password = customer.Password;

                context.SaveChanges();
            }
        }

        public static void DeleteCustomer(int customerID)
        {
            using var context = new FuminiHotelManagementContext();
            Customer? existingCustomer = context.Customers.FirstOrDefault(c => c.CustomerId == customerID);
            if (existingCustomer != null)
            {
                existingCustomer.CustomerStatus = 2;
                context.SaveChanges();
            }
        }

    }
}
